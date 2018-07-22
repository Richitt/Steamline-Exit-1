using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidMovement : MonoBehaviour {

    private Animator animator;

    private Rigidbody2D body;

    public float horizontalSpeed;
    public float jumpSpeed;
    private bool gasState;
    private bool liquidState;
    private bool solidState;

    private Collider2D vCollider;
    private ChangeState changeState;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vCollider = GetComponent<CapsuleCollider2D>();
        changeState = GetComponent<ChangeState>();
    }

    // Update is called once per frame
    void Update ()
    {
        bool grounded = Grounded();
        /////////////////////////////////////////
        //TODO: Debug Water State Changes
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
        //////////////////////////////////////////

        int hDirection = 0;
        if (Input.GetKey("left"))
        {
            hDirection--;
        }
        if (Input.GetKey("right"))
        {
            hDirection++;
        }
        body.velocity = new Vector2(hDirection * horizontalSpeed, body.velocity.y);

        if (Input.GetKey("down"))
        {
            body.velocity = new Vector2(0, -jumpSpeed);
        }
        if (Input.GetKeyDown("up") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        // animate with blend tree
        animator.SetFloat("VelocityX", hDirection);
        if (hDirection != 0)
        {
            animator.SetFloat("FacingX", hDirection);
        }
        animator.SetFloat("VelocityY", Mathf.Sign(body.velocity.y));
        animator.SetBool("Grounded", grounded);
    }

    public bool Grounded()
    {
        return CheckRaycastGround(Vector2.zero) ||
            CheckRaycastGround(Vector2.left * (vCollider.bounds.extents.x + vCollider.offset.x)) || 
            CheckRaycastGround(Vector2.right * (vCollider.bounds.extents.x + vCollider.offset.x));
    }

    public bool CheckRaycastGround(Vector2 pos)
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
