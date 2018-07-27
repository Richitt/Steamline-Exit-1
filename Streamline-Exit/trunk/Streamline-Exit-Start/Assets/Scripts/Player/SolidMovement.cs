using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidMovement : LiquidMovement
{
    // Do the same thing as liquid, but we need to flip sprite when facing right
    protected override void Update()
    {
        body.gravityScale = 1.5f;
        body.drag = 0f;
        sr.flipX = false;
        //TODO: Debug Water State Changes
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("hit space");
        }
        if (Input.GetKeyDown("1"))
        {
            changeState.SetState(ChangeState.SOLID);
        }
        if (Input.GetKeyDown("2"))
        {
            changeState.SetState(ChangeState.LIQUID);
        }
        if (Input.GetKeyDown("3"))
        {
            changeState.SetState(ChangeState.GAS);
        }

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
            body.velocity = new Vector2(hDirection * horizontalSpeed, body.velocity.y);

            if (Input.GetKey("down"))
            {
                body.velocity = new Vector2(0, -25);
            }
            if (Input.GetKeyDown("up") && Jumpable())
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }
        else
        {
            // SceneTransition trans = transObj.GetComponent<SceneTransition>();
            //switch (trans.side)
            //{
            //   case SceneTransitions.Side.LEFT:
            //       hDirection = -1;
            //      break;
            //   case SceneTransitions.Side.RIGHT:
            //        hDirection = 1;
            //        break;
            //   }
            //  body.velocity = new Vector2(hDirection * horizontalSpeed * 0.5f, body.velocity.y);
        }

        // animate with blend tree
        animator.SetFloat("VelocityX", hDirection);
        if (hDirection != 0)
        {
            animator.SetFloat("FacingX", hDirection);
            facingX = hDirection;
        }
        animator.SetFloat("VelocityY", Mathf.Sign(body.velocity.y));
    }

    // player can't jump when solid
    protected override bool Jumpable()
    {
        return false || Input.GetKey(KeyCode.LeftControl);
    }

    protected override bool Movable()
    {
        return waterMeter.Size != WaterMeter.SMALL || Input.GetKey(KeyCode.LeftControl);
    }
}
