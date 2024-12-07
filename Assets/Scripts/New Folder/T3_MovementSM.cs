using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_MovementSM : T3_StateMachine
{
    [HideInInspector]
    public T3_Idle idleState;
    [HideInInspector]
    public T3_Moving movingState;
    [HideInInspector]
    public T3_Jumping jumpingState;

    public Rigidbody2D rb2d;
    public float speed = 5f;
    public SpriteRenderer spriteRenderer;
    
    public float jumpForce = 10f;

    private void Awake()
    {
        idleState = new T3_Idle(this);
        movingState = new T3_Moving(this);
        jumpingState = new T3_Jumping(this);
    }
    
    protected override T3_BaseState GetInitialState()
    {
        return idleState;
    }
}
