using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class T3_Grounded : T3_BaseState
{
    protected T3_MovementSM _sm;

    public T3_Grounded(string name, T3_MovementSM stateMachine) : base(name, stateMachine)
    {
        _sm = (T3_MovementSM)stateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(_sm.jumpingState);
    }
}