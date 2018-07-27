using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public const int SOLID = 0;
    public const int LIQUID = 1;
    public const int GAS = 2;

    public int State = LIQUID;

    private Animator animator;
    private CinemachineVirtualCamera cam;

    // make the player single
    // oh wait we all already are
    private static ChangeState Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            gameObject.tag = "Player";
        }
        else if (Instance != this)
        {
            cam = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
            cam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        SetState(State);
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
