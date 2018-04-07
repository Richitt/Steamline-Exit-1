using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputBehaviour : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    static System.Random random = new System.Random();

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
            GameObject prefab = prefabs[random.Next(prefabs.Count)];
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Instantiate(prefab, mousePos, Quaternion.identity);
        }
    }
}
