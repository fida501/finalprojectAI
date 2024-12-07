using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_BaseState
{
    public string name;
    protected T3_StateMachine stateMachine;
    
     public T3_BaseState(string name, T3_StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    
    public virtual void Enter(){ }
    public virtual void UpdateLogic(){ }
    public virtual void UpdatePhysics(){ }
    public virtual void Exit(){ }
}
