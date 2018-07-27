using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollector : MonoBehaviour {

	[HideInInspector] public int objective = 0;

	void Update ()
	{
		
		if (objective == 1) {
			Debug.Log ("Good show m8");
			objective++;
		} else {
			Debug.Log (objective);
		}
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Player") 
		{
			Debug.Log ("TOUCHA TOUCHA TOUCHA TOUCH ME I WANNA FEEL DIRTY");
			if (this.gameObject.name == "Key") 
			{
				objective++;
			}
			this.gameObject.SetActive(false);
		}
	}
}
