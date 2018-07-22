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
	void Update ()
    {
		
	}

    public void SetState(int state)
    {
        if (State == state)
        {
            return;
        }
        State = state;
        animator.SetInteger("State", State);
    }
}
