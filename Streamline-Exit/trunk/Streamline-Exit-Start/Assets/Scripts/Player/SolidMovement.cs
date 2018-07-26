using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidMovement : LiquidMovement
{
    // Do the same thing as liquid, but we need to flip sprite when facing right
    protected override void Update()
    {
        base.Update();
        sr.flipX = facingX == 1;
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
