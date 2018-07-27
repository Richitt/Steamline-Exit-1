using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMovement : LiquidMovement
{
    // Do the same thing as liquid, but we need to flip sprite when facing right
    protected override void Update()
    {
        // no gravity
        body.gravityScale = 0.3f;
        body.drag = 1f;
        // can't move if transition exists
        int hDirection = 0;
        GameObject transObj = GameObject.FindGameObjectWithTag("SceneTransition");
        if (transObj == null)
        {
            if (Input.GetKey("left") && Movable())
            {
                hDirection--;
            }
            if (Input.GetKey("right") && Movable())
            {
                hDirection++;
            }
            if (hDirection != 0)
            {
                body.velocity = new Vector2(hDirection * horizontalSpeed, body.velocity.y);
            }

            if (Input.GetKey("down") && Movable())
            {
                Debug.Log("You're at the mercy of the wind sorry");
            }
            if (Input.GetKey("up") && Movable())
            {
                body.velocity = new Vector2(body.velocity.x, horizontalSpeed);
            }
        }
        else
        {
            SceneTransition trans = transObj.GetComponent<SceneTransition>();
            switch (trans.side)
            {
                case SceneTransitions.Side.LEFT:
                    hDirection = -1;
                    break;
                case SceneTransitions.Side.RIGHT:
                    hDirection = 1;
                    break;
            }
            body.velocity = new Vector2(hDirection * horizontalSpeed * 0.5f, body.velocity.y);
        }

        // animate with blend tree. No point in doing this but oh well
        animator.SetFloat("VelocityX", hDirection);
        if (hDirection != 0)
        {
            animator.SetFloat("FacingX", hDirection);
            facingX = hDirection;
        }
        animator.SetFloat("VelocityY", Mathf.Sign(body.velocity.y));
        animator.SetBool("Grounded", false);
        // set direction
        sr.flipX = facingX == 1;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit wind");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("hello");
        string checkere = other.gameObject.name;
        //Debug.Log(checkere);
        if (checkere.Equals("WindUpBox"))
        {
            Debug.Log("hit it here boy");
            Vector2 dir = new Vector2(0, 1);
            GetComponent<Rigidbody2D>().AddForce(dir * 15);
        }
        if (checkere.Equals("WindUpBoxStrong"))
        {
            Vector2 dir = new Vector2(0, 1);
            GetComponent<Rigidbody2D>().AddForce(dir * 50);
        }
    } 

    protected override bool Movable()
    {
        return true;
    }
}
