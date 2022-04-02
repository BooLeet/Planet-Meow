using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardInput : PlayerInput
{
    protected override Vector2 GetMoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
