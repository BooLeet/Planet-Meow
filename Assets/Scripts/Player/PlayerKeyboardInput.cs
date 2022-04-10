using UnityEngine;

public class PlayerKeyboardInput : PlayerInput
{
    public KeyCode pauseButton = KeyCode.Escape;

    protected override void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            InvokePause();
        }
        base.Update();
    }

    protected override Vector2 GetMoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    protected override bool GetAttack()
    {
        return GetMoveInput().magnitude > 0;
    }
}
