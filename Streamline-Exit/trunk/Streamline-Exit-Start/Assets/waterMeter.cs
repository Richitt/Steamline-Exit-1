using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterMeter : MonoBehaviour {
    public Texture2D guage;
    public Texture2D filler;
    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(20, 60);

    public bool filling = false;
    [HideInInspector] public float amount = 50;
    float fillSpeed = 0.1f;
    float drainSpeed = 0.1f;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnGUI()
    {
        //TODO: this is the debug GUI
        GUI.Label(new Rect(0, 0, 500, 100), "Water Amount : " + amount);
        GUI.Label(new Rect(0, 15, 500, 100), "Drate is on water : " + filling);
        if (amount == 0)
        {
          GUI.Label(new Rect(Screen.width/2, Screen.height/2, 500, 100),
          "You have killed Drate. You monster.");
        }
        // draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), guage);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, (size.y - (size.y * amount/100)), size.x, size.y * amount/100));
        GUI.Box(new Rect(0, -size.y + (size.y * amount/100), size.x, size.y), filler);
        GUI.EndGroup();
        GUI.EndGroup();
    }
    void Update()
    {
        // if (col.gameObject.tag == "wet")
        // {
        //   filling = true;
        // }
        // else
        // {
        //   filling = false;
        // }
        //Either fill or empty the guage
        if (filling && amount < 100)
        {
            amount += fillSpeed;
        }
        else if (!filling && amount>0)
        {
            amount -= drainSpeed;
        }

        //Check for boundaries in the percentages
        if (amount > 100)
        {
            amount = 100;
        }
        else if (amount < 0)
        {
            amount = 0;
        }

    }

    //check for Water platform contact
    void OnCollisionEnter2D(Collision2D col)
    {
        filling = (col.gameObject.tag == "wet");
    }

    void OnCollisionExit2D(Collision2D col)
    {
        filling = false;
    }

}
