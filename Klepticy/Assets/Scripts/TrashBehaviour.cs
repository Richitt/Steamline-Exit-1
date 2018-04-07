using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{
    public enum ColorType
    {
        None, Orange, Blue, Purple, Red, Green, Yellow
    }

    static Array values = Enum.GetValues(typeof(ColorType));
    static System.Random random = new System.Random();

    SpriteRenderer spriteRenderer;
    public ColorType colorType = ColorType.None;

    // adjacency list of all collisions of same color
    Dictionary<TrashBehaviour, bool> adj = new Dictionary<TrashBehaviour, bool>();

    public bool landed = false;

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (colorType == ColorType.None)
        {
            colorType = (ColorType)values.GetValue(random.Next(values.Length - 1) + 1);
            switch (colorType)
            {
                case ColorType.Orange:
                    spriteRenderer.color = new Color(1, 0.65f, 0);
                    break;
                case ColorType.Blue:
                    spriteRenderer.color = Color.blue;
                    break;
                case ColorType.Purple:
                    spriteRenderer.color = Color.magenta;
                    break;
                case ColorType.Red:
                    spriteRenderer.color = Color.red;
                    break;
                case ColorType.Green:
                    spriteRenderer.color = Color.green;
                    break;
                case ColorType.Yellow:
                    spriteRenderer.color = Color.yellow;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // since this is called after OnCollisionEnter/OnCollisionExit,
        // breadth first search to try to destroy objects
        Dictionary<TrashBehaviour, bool> traversed = new Dictionary<TrashBehaviour, bool>();
        Queue<TrashBehaviour> openSet = new Queue<TrashBehaviour>();
        openSet.Enqueue(this);
        // while there are more nodes to process
        while (openSet.Count > 0)
        {
            // get the next element
            TrashBehaviour element = openSet.Dequeue();
            // if we already traversed the node, continue
            bool alreadySeen = false;
            traversed.TryGetValue(element, out alreadySeen);
            if (element.gameObject == null || alreadySeen)
            {
                continue;
            }
            // otherwise, add all connected nodes and mark this node as traversed
            traversed[element] = true;
            foreach (KeyValuePair<TrashBehaviour, bool> entry in element.adj)
            {
                if (entry.Value)
                {
                    openSet.Enqueue(entry.Key);
                }
            }
        }
        // now destroy everything traversed if there are more than 4 elements connected of same color
        if (traversed.Count >= 4)
        {
            foreach (KeyValuePair<TrashBehaviour, bool> entry in traversed)
            {
                Destroy(entry.Key.gameObject);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        GameObject gameObject = collision.collider.gameObject;
        if (gameObject.tag == "Trash")
        {
            TrashBehaviour trash = gameObject.GetComponent<TrashBehaviour>();
            // this has landed if it has touched another trash obj
            if (trash.landed)
            {
                landed = true;
            }
        }
        // it has also landed if it touches the ground
        if (gameObject.tag == "Floor")
        {
            landed = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Trash")
        {
            TrashBehaviour trash = gameObject.GetComponent<TrashBehaviour>();
            // add to adjacency matrix if it's the same color and it entered collision
            if (trash.colorType == colorType && landed)
            {
                adj[trash] = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // remove from adjacency matrix if it's the same color and it's exited collision
        GameObject gameObject = collision.collider.gameObject;
        if (gameObject.tag == "Trash")
        {
            TrashBehaviour trash = gameObject.GetComponent<TrashBehaviour>();
            if (trash.colorType == colorType && landed)
            {
                adj[trash] = false;
            }
        }
    }
}
