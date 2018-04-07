using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputBehaviour : MonoBehaviour
{
    public GameObject prefab;
    public GameObject line;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // create a square on click
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Instantiate(prefab, mousePos, Quaternion.identity);
        }
        // track the highest
        float maxYPos = -100;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Trash"))
        {
            TrashBehaviour trash = gameObject.GetComponent<TrashBehaviour>();
            if (trash.landed)
            {
                Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
                float yPos = (bounds.center + bounds.extents).y;
                if (maxYPos < yPos)
                {
                    maxYPos = yPos;
                }
            }
        }
        line.transform.position = new Vector3(line.transform.position.x, maxYPos, line.transform.position.z);
    }
}
