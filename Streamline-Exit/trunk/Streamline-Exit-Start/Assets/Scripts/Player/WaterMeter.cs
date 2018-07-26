using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMeter : MonoBehaviour
{
    public Texture2D guage;
    public Texture2D filler;
    public Vector2 indicatorPos = new Vector2(20, 40);
    public Vector2 indicatorSize = new Vector2(20, 60);

    public bool filling = false;
    [HideInInspector] public float amount = 50;
    float fillSpeed = 0.1f;
    float drainSpeed = 0.05f;

    private Collider2D vCollider;
    private Animator animator;

    public const int SMALL = -1;
    public const int NORMAL = 0;
    public const int LARGE = 1;
    public int Size = 0;

    void Start()
    {
        vCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void OnGUI()
    {
        //TODO: this is the debug GUI
        GUI.Label(new Rect(0, 0, 500, 100), "Water Amount : " + amount);
        GUI.Label(new Rect(0, 15, 500, 100), "Drate is on water : " + filling);
        if (amount == 0)
        {
          GUI.Label(new Rect(Screen.width/2, Screen.height/2, 500, 100),
          "You have killed Drate. You monster.");
        }
        // draw the background:
        GUI.BeginGroup(new Rect(indicatorPos.x, indicatorPos.y, indicatorSize.x, indicatorSize.y));
        GUI.Box(new Rect(0, 0, indicatorSize.x, indicatorSize.y), guage);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, (indicatorSize.y - (indicatorSize.y * amount/100)), indicatorSize.x, indicatorSize.y * amount/100));
        GUI.Box(new Rect(0, -indicatorSize.y + (indicatorSize.y * amount/100), indicatorSize.x, indicatorSize.y), filler);
        GUI.EndGroup();
        GUI.EndGroup();
    }
    void Update()
    {
        //Either fill or empty the guage
        if (filling && amount < 100)
        {
            amount += fillSpeed;
        }
        else if (!filling && amount>0)
        {
            amount -= drainSpeed;
        }

        //Check for boundaries in the percentages
        amount = Mathf.Clamp(amount, 0f, 100f);

        // set animation
        if (amount <= 30)
        {
            animator.SetInteger("Size", SMALL);
            Size = SMALL;
        }
        else if (amount >= 60)
        {
            animator.SetInteger("Size", LARGE);
            Size = LARGE;
        }
        else
        {
            animator.SetInteger("Size", NORMAL);
            Size = NORMAL;
        }
    }

    //check for Water platform contact
    void OnCollisionEnter2D(Collision2D col)
    {
        // only use filling logic for one collider
        if (col.otherCollider == vCollider)
        {
            filling = (col.gameObject.tag == "Wet");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        // only use filling logic for one collider
        if (col.otherCollider == vCollider)
        {
            filling = false;
        }
    }
}
