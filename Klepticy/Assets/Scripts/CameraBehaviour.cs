using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject follow;
    float ySpeed = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // follow the specified object
        // to smooth the following, use a differential equation
        // where acceleration makes spd approach targetSpd = k1 * distanceToTravel
        // acceleration is k2 * spdDifference
        float targetPos = Mathf.Max(0, follow.transform.position.y);
        float currentPos = transform.position.y;
        float k1 = 0.1f;
        float targetSpd = k1 * (targetPos - currentPos);
        float k2 = 0.1f;
        float acc = k2 * (targetSpd - ySpeed);
        ySpeed += acc;
        currentPos += ySpeed;
        
        transform.position = new Vector3(transform.position.x,
            currentPos, transform.position.z);
	}
}
