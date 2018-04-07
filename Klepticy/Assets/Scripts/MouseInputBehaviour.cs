using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputBehaviour : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    static System.Random random = new System.Random();

    int counter = 0;

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
            int n = random.Next(prefabs.Count);
            n = 1;
            GameObject prefab = prefabs[n];
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = -10;
            GameObject go = Instantiate(prefab, mousePos, Quaternion.identity);
            counter++;
            if (counter == 10)
            {
                counter = 0;
                PunPrinter.PrintPun(Int32.Parse(go.name.Split('_')[0]));
            }
        }
    }
}
