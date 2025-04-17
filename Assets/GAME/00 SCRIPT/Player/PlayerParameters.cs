using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Normal,
    Immortal,
    Dead
}

[Serializable]
public class PlayerParameters
{
    [SerializeField] private float jumpForce = 17f;
    
    public PlayerState State { get; set; }

    public PlayerParameters(float jumpForce = 17f,  PlayerState playerState = PlayerState.Normal)
    {
        this.jumpForce = jumpForce;
        this.State = playerState;
    }

    // Shortcut properties
    public bool IsAlive => State != PlayerState.Dead;
    public bool IsImmortal => State == PlayerState.Immortal;
    
    public float GetJumpForce()
    {
        return jumpForce;
    }
}
