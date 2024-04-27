using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardInput : PlayerInput
{
    protected override Vector2 GetMoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    protected override bool GetAttack()
    {
        return Input.GetMouseButton(0);
    }
}
