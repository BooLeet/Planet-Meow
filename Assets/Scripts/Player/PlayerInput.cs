using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    public Player player;
    
    protected void Update()
    {
        player.Move(GetMoveInput());
    }

    protected abstract Vector2 GetMoveInput();
}
