using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public const int SOLID = 0;
    public const int LIQUID = 1;
    public const int GAS = 2;

    public int State { get; private set; }

    private Animator animator;
    
    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        State = 1;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SetState(SOLID);
        }
        if (Input.GetKeyDown("2"))
        {
            SetState(LIQUID);
        }
        if (Input.GetKeyDown("3"))
        {
            SetState(GAS);
        }
    }

    public void SetState(int state)
    {
        if (State == state)
        {
            return;
        }

        // remove old components. Make sure everything is a child of LiquidMovement or else this won't work
        LiquidMovement old = gameObject.GetComponent<LiquidMovement>();
        float horizontalSpeed = old.horizontalSpeed;
        float jumpSpeed = old.jumpSpeed;
        Destroy(old);

        State = state;
        animator.SetInteger("State", State);
        // add new components
        LiquidMovement movement = null;
        switch (State)
        {
            case SOLID:
                movement = gameObject.AddComponent<SolidMovement>();
                break;
            case LIQUID:
                movement = gameObject.AddComponent<LiquidMovement>();
                break;
            case GAS:
                // movement = gameObject.AddComponent<GasMovement>();
                break;
            default:
                throw new System.Exception("That's not a valid state. :frogs:");
        }
        if (movement != null)
        {
            movement.horizontalSpeed = horizontalSpeed;
            movement.jumpSpeed = jumpSpeed;
        }
    }
}
