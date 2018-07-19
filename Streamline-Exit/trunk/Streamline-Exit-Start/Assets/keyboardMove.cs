using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardMove : MonoBehaviour {

    [HideInInspector] public Animator animator;

    private Rigidbody2D body;
    private SpriteRenderer sprRend;

    private bool grounded;
    public float horizonalSpeed;
    public float jumpSpeed;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        grounded = true; //TODO: not
        horizonalSpeed = 1.5f;
        jumpSpeed = 4f;
    }

    // Update is called once per frame
    void Update () {

        if (body.velocity.y == 0)
        {
          grounded = true;
        }
        /////////////////////////////////////////
        //TODO: Debug Water State Changes
        if (Input.GetKeyDown("1"))
        {
           animator.SetInteger("waterState", 0);
        }
        if (Input.GetKeyDown("2"))
        {
            animator.SetInteger("waterState", 1);
        }
        if (Input.GetKeyDown("3"))
        {
            animator.SetInteger("waterState", 2);
        }
        //////////////////////////////////////////

        if (Input.GetKey("left"))
        {
            sprRend.flipX = false;
            body.velocity = new Vector2(-horizonalSpeed, body.velocity.y);
        }

        if (Input.GetKey("right"))
        {
            sprRend.flipX = true;
            body.velocity = new Vector2(horizonalSpeed, body.velocity.y);
        }
        if (Input.GetKey("down"))
        {
            body.velocity = new Vector2(0, -jumpSpeed);
        }
        if (Input.GetKeyDown("up") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            grounded = false;
        }

        animator.SetBool("jump", body.velocity.y != 0);
        animator.SetBool("walk", body.velocity.x != 0);

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
