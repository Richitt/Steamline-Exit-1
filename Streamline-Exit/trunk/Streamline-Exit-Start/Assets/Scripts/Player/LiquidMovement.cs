using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float jumpSpeed;
    private bool gasState;
    private bool liquidState;
    private bool solidState;
    private float currentf;
    private Sprite[] gasSprites;

    protected Animator animator;
    protected Rigidbody2D body;
    protected SpriteRenderer sr;
    protected Collider2D vCollider;
    protected ChangeState changeState;
    protected WaterMeter waterMeter;

    protected int facingX = -1;

    protected virtual void Start()
    {
        currentf = 0.9f;
        gasSprites = Resources.LoadAll<Sprite>("gasMode");
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
        body.gravityScale = currentf;
        body.drag = 0f;
        sr.flipX = false;
        bool grounded = Grounded();
        //TODO: Debug Water State Changes
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("hit space");
            gasState = true;
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
                Debug.Log("Nothing, you ain't an ice cube");
            }
            if (Input.GetKeyDown("up") && Jumpable())
            {
                body.velocity = new Vector2(body.velocity.x+10, jumpSpeed);
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
        animator.SetBool("Grounded", grounded);
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
        if (checkere.Equals("WindUpBox") && gasState == true)
        {
            Debug.Log("hit it here boy");
            Vector2 dir = new Vector2(0, 1);
            GetComponent<Rigidbody2D>().AddForce(dir * 15);
        }
        if (checkere.Equals("WindUpBoxStrong") && gasState == true)
        {
            Debug.Log("hit it here boy");
            Vector2 dir = new Vector2(0, 1);
            GetComponent<Rigidbody2D>().AddForce(dir * 50);
        }
        if (checkere.Equals("glacieSolid"))
        {
            gasState = false;
            currentf = 1.2f;
            solidState = true;
        }
        if (checkere.Equals("glacieSolid2.0"))
        {
            gasState = false;
            currentf = 1.2f;
            solidState = true;
        }
        if (checkere.Equals("ToasterMan"))
        {
            Debug.Log("hit the toast");
            gasState = true;
            GetComponent<SpriteRenderer>().sprite = gasSprites[0];
            currentf = 0.3f;
        }
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



//Vector3 colliderPointion = GetComponent<Collider2D>().transform.position;
//Debug.Log(colliderPointion);
//Vector3 position = transform.position;
//Vector3 targetPosition = colliderPointion;
//Vector3 direction = targetPosition - position;
//Collider2D col = other;
//other.attachedRigidbody.velocity = new Vector2(-1, -1);
//Debug.Log(col.transform.position);
//Debug.Log("here's the velocity " + col.attachedRigidbody.velocity);
//direction.Normalize();
//int moveSpeed = 10;
//targetPosition += direction * moveSpeed * Time.deltaTime;