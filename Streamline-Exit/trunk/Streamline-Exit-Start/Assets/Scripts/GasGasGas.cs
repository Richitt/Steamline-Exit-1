using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGasGas : MonoBehaviour {

    private Rigidbody2D body;
    public Animator animator;
    private bool gas;
    private bool grounded;
    public float horizonalSpeed;
    public float jumpSpeed;
    private SpriteRenderer sprRend;
    Collider2D gasCheck;
    private void Awake()
    {
        gas = true;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        horizonalSpeed = 1.5f;
        jumpSpeed = 2f;
        body.gravityScale = 0.1f *body.gravityScale;
        gasCheck = GetComponent<Collider2D>();
        //body.AddForce(-Physics.gravity * body.mass);
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////////////

        if (Input.GetKey("a"))
        {
            sprRend.flipX = false;
            body.velocity = new Vector2(-horizonalSpeed, body.velocity.y);
        }

        if (Input.GetKey("d"))
        {
            sprRend.flipX = true;
            body.velocity = new Vector2(horizonalSpeed, body.velocity.y);
        }
        if (Input.GetKey("s"))
        {
            body.velocity = new Vector2(0, -jumpSpeed);
        }
        if (Input.GetKeyDown("w") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        animator.SetBool("jump", body.velocity.y != 0);
        animator.SetBool("walk", body.velocity.x != 0);
        if (Input.GetKeyDown(KeyCode.Space) && gas == true)
        {
            gasCheck.enabled = !gasCheck.enabled;
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
