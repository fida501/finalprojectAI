using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_Idle : T3_Grounded
{
    private float _horizontalInput;

    public T3_Idle(T3_MovementSM stateMachine) : base("Idle", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        ((T3_MovementSM)stateMachine).spriteRenderer.color = Color.red;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
            stateMachine.ChangeState(((T3_MovementSM)stateMachine).movingState);
    }
}