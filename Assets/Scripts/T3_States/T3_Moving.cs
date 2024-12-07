using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class T3_Moving : T3_Grounded
{
    private float _horizontalInput;

    public T3_Moving(T3_MovementSM stateMachine) : base("Moving", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        _sm.spriteRenderer.color = Color.green;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
            stateMachine.ChangeState(_sm.idleState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 vel = _sm.rb2d.velocity;
        vel.x = _horizontalInput * _sm.speed;
        _sm.rb2d.velocity = vel;
    }
}