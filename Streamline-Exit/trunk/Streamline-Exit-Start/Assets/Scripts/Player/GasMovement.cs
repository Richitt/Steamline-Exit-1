using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMovement : LiquidMovement
{
    public int timer = 0;
    private int jumpTimes = 0;
    private bool moveRightWeakened = false;
    // Do the same thing as liquid, but we need to flip sprite when facing right
    protected override void Start()
    {
        
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vCollider = GetComponent<CapsuleCollider2D>();
        changeState = GetComponent<ChangeState>();
        waterMeter = GetComponent<WaterMeter>();

        
    }
    protected override void Update()
    {
        // no gravity
        timer++;
        body.gravityScale = 0.3f;
        body.drag = 1f;
       // bool grounded = Grounded();
        // can't move if transition exists
        double hDirection = 0;
        GameObject transObj = GameObject.FindGameObjectWithTag("SceneTransition");
        if(Jumpable() == true)
        {
            jumpTimes = 0;
        }
        if (transObj == null)
        {
            if (Input.GetKey("left") && Movable())
            {
                hDirection--;
            }
            if (Input.GetKey("right") && Movable())
            {
                if (moveRightWeakened == false)
                {
                    hDirection++;
                }
                else
                {
                    hDirection = hDirection + 0.001;
                }
            }
            if (hDirection != 0)
            {
                body.velocity = new Vector2((float)(hDirection * horizontalSpeed), body.velocity.y);
            }

            if (Input.GetKey("down") && Movable())
            {
                Debug.Log("You're at the mercy of the wind sorry");
            }
            if (Input.GetKeyDown("up") && Movable())
            {
                if (jumpTimes < 5)
                {
                    body.velocity = new Vector2(body.velocity.x, horizontalSpeed);
                    jumpTimes++;
                }
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
            body.velocity = new Vector2((float)(hDirection * horizontalSpeed * 0.5f), body.velocity.y);
        }

        // animate with blend tree. No point in doing this but oh well
        animator.SetFloat("VelocityX", (float)(hDirection));
        if (hDirection != 0)
        {
            animator.SetFloat("FacingX", (float)(hDirection));
            facingX = (int)(hDirection);
        }
        animator.SetFloat("VelocityY", Mathf.Sign(body.velocity.y));
        animator.SetBool("Grounded", false);
        // set direction
        sr.flipX = facingX == 1;
    }
    protected override  bool Jumpable()
    {
        return Grounded();
    }

    private  bool Grounded()
    {
        return CheckRaycastGround(Vector2.zero) ||
            CheckRaycastGround(Vector2.left * (vCollider.bounds.extents.x + vCollider.offset.x)) ||
            CheckRaycastGround(Vector2.right * (vCollider.bounds.extents.x + vCollider.offset.x));
    }

    private bool CheckRaycastGround(Vector2 pos)
    {
        // player has 2 colliders, so to find more, make this 3
        RaycastHit2D[] results = new RaycastHit2D[3];
        // raycast for a collision down
        Physics2D.Raycast((Vector2)transform.position + pos, Vector2.down, new ContactFilter2D(), results,
            vCollider.bounds.extents.y - vCollider.offset.y + 0.1f);
        // make sure raycast hit isn't only player
        foreach (RaycastHit2D result in results)
        {
            if (result != null && result.collider != null)
            {
                if (result.collider.gameObject.tag != "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("pittoo");
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
            GetComponent<Rigidbody2D>().AddForce(dir * 10);
        }
        if (checkere.Equals("WindUpBoxStrong"))
        {
            Vector2 dir = new Vector2(0, 1);
            GetComponent<Rigidbody2D>().AddForce(dir * 50);
        }
        if (checkere.Equals("WindUpBoxLeftStrong"))
        {
            Vector2 dir = new Vector2(-1, -1);
            GetComponent<Rigidbody2D>().AddForce(dir * 50);
        }
        if (checkere.Equals("WindUpBoxDownStrong"))
        {
            Vector2 dir = new Vector2(0, -1);
            GetComponent<Rigidbody2D>().AddForce(dir * 50);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        string checkere = collision.gameObject.name;
        if (checkere.Equals("WindUpBoxLeftStrong"))
        {
            moveRightWeakened = true;
            Vector2 dir = new Vector2(-1, 0);
            GetComponent<Rigidbody2D>().AddForce(dir * 50);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        moveRightWeakened = false;
    }

    protected override bool Movable()
    {
        return true;
    }
}
