using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipperBehaviour : MonoBehaviour
{

    Animator animator;

    float spd = 0.1f;
    float stationarySpd = 0.05f;

    // Use this for initialization
    void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CameraBehaviour.zipperTarget.y < -10 || CameraBehaviour.zipperTarget == Vector3.zero)
        {
            return;
        }
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        // stay in idle if idle
        if (!state.IsName("Base.ZipperIdle"))
        {
            // move racoon toward new position
            transform.position = Vector3.MoveTowards(transform.position, CameraBehaviour.zipperTarget, spd);
            // if the distance from self to the point is less than stationarySpd
            if (Vector2.Distance(transform.position, CameraBehaviour.zipperTarget) <= stationarySpd)
            {
                animator.SetBool("Climbing", false);
            }
        }
        else
        {
            // if the distance from self to the point is more than stationarySpd
            if (Vector2.Distance(transform.position, CameraBehaviour.zipperTarget) >= stationarySpd)
            {
                animator.SetBool("Climbing", true);
            }
            else
            {
                // move racoon to new position
                transform.position = CameraBehaviour.zipperTarget;
            }
        }
    }
}
