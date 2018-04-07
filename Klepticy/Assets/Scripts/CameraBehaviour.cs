using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject follow;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // follow the specified object
        transform.position = new Vector3(transform.position.x,
            Mathf.Max(follow.transform.position.y, 0), transform.position.z);
	}
}
