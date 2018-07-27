using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollector : MonoBehaviour {
	/**
	 * TODO: so if you go back into the room it doesn't kill itself?
Why not make a static dictionary
and access it
and then on Start, if the dictionary entry is set to true or something, Destroy(gameObject)
	*/

	[HideInInspector] public bool keyGet = false;

	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Player") 
		{
			if (this.gameObject.name == "Key") 
			{
				keyGet = true;
			}

			transform.GetComponent<Collider2D>().enabled = false;
			transform.GetComponent<Renderer>().enabled = false;


		}
	}
}
