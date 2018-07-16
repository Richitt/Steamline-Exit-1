using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardMove : MonoBehaviour {

    private Rigidbody2D body;
    private bool grounded;
    public float horizonalSpeed;
    public float jumpSpeed;


    private void Awake()
    {
        grounded = true; //TODO: not
        body = GetComponent<Rigidbody2D>();
        horizonalSpeed = 1.5f;
        jumpSpeed = 2f;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("left"))
        {
            body.velocity = new Vector2(-horizonalSpeed, body.velocity.y);
        }

        if (Input.GetKeyDown("right"))
        {
            body.velocity = new Vector2(horizonalSpeed, body.velocity.y);
        }
        if (Input.GetKey("down"))
        {
            body.velocity = new Vector2(0, -jumpSpeed);
        }
        if (Input.GetKeyDown("up") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }
   
    //Handling collisions here
    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.tag);
        if (col.gameObject.tag == "wall")
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "wall")
        {
            grounded = false;
        }
    }


}
