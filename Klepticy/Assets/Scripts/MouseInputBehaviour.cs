using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputBehaviour : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    static System.Random random = new System.Random();
    Rigidbody2D rb;
    public float moveScale = 0.1f;
    public float gravScale = 0.05f;

    // Use this for initialization
    void Start ()
    {
        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        Vector3 vel = rb.velocity;
        // create a square on click
        if (Input.GetKey("up"))
        {
            vel.y = moveScale;
        }

       if (Input.GetKey("down"))
        {
            vel.y = -moveScale;
        }

        if (Input.GetKey("left"))
        {
            vel.x = moveScale;
        }

        if (Input.GetKey("right"))
        {
            vel.x = -moveScale;
        }

        if (vel.x > 0)
        {
            vel.x -= 0.01f;
        }

        if (vel.y > 0)
        {
            vel.y -= 0.01f;
        }

        rb.velocity = vel;
        //for evey update this.pos.y-=1;


    }
}
