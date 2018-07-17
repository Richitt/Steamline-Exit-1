﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardMove : MonoBehaviour {

    private Rigidbody2D body;
    public Animator animator;
    private bool grounded;
    public float horizonalSpeed;
    public float jumpSpeed;
    private SpriteRenderer sprRend;
    private void Awake()
    {
        grounded = true; //TODO: not
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        horizonalSpeed = 1.5f;
        jumpSpeed = 2f;
    }

    // Update is called once per frame
    void Update () {

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