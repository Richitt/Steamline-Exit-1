using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liquidWalk : MonoBehaviour {

    private SpriteRenderer spr;
    private Sprite[] sprites;
    // Use this for initialization
    void Start () {
        spr = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("liquidDrate");

    }
	
	// Update is called once per frame
	void Update () {

    }
}
