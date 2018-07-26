using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float jumpSpeed;

    protected Animator animator;
    protected Rigidbody2D body;
    protected SpriteRenderer sr;
    protected Collider2D vCollider;
    protected ChangeState changeState;
    protected WaterMeter waterMeter;

    protected int facingX = -1;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vCollider = GetComponent<CapsuleCollider2D>();
        changeState = GetComponent<ChangeState>();
        waterMeter = GetComponent<WaterMeter>();
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        sr.flipX = false;
        bool grounded = Grounded();

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
                body.velocity = new Vector2(0, -jumpSpeed);
            }
            if (Input.GetKeyDown("up") && Jumpable())
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
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

        // animate with blend tree
        animator.SetFloat("VelocityX", hDirection);
        if (hDirection != 0)
        {
            animator.SetFloat("FacingX", hDirection);
            facingX = hDirection;
        }
        animator.SetFloat("VelocityY", Mathf.Sign(body.velocity.y));
        animator.SetBool("Grounded", grounded);
    }

    protected virtual bool Jumpable()
    {
        return Grounded();
    }

    protected virtual bool Movable()
    {
        return true;
    }

    private bool Grounded()
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
}
