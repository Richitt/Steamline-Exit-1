using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMan : MonoBehaviour {
    private string checker = "cloud";
	// Use this for initialization
	void Start () {
        Debug.Log("we started");
	}
	
	// Update is called once per frame
	void Update () {
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hesllo");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("hello");
        //Vector3 colliderPointion = GetComponent<Collider2D>().transform.position;
        //Debug.Log(colliderPointion);
        // Here you add negative forces to object that is within the fan area
        // Other is the object, that should be pushed away
        //Vector3 position = transform.position;
        //Vector3 targetPosition = colliderPointion;
        //Vector3 direction = targetPosition - position;
        //Collider2D col = other;
        //other.attachedRigidbody.velocity = new Vector2(-1, -1);
        //Debug.Log(col.transform.position);
        //Debug.Log("here's the velocity " + col.attachedRigidbody.velocity);
        //direction.Normalize();
        //int moveSpeed = 10;
        //targetPosition += direction * moveSpeed * Time.deltaTime;

    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("hefllo");
    }
}
