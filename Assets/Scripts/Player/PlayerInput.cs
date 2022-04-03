using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    public Player player;
    
    protected void Update()
    {
        player.Move(GetMoveInput());

        if (GetAttack())
        {
            player.Attack();
        }
    }

    protected abstract Vector2 GetMoveInput();

    protected abstract bool GetAttack();
}
