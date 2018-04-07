using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputBehaviour : MonoBehaviour
{
    public GameObject prefab;

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
        float time = Time.realtimeSinceStartup;
        float maxYPos = float.MinValue;
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Trash"))
        {
            Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
            float yPos = (bounds.center - bounds.extents).y;
            if (maxYPos < yPos)
            {
                maxYPos = yPos;
            }
        }
        float total = Time.realtimeSinceStartup - time;
        Debug.Log(maxYPos + " " + total);
    }
}
