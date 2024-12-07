using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class T3_Jumping : T3_BaseState
{
    protected T3_MovementSM _sm;
    private bool _isGrounded;
    private int _groundlayer = 1 << 6;

    public T3_Jumping(T3_MovementSM stateMachine) : base("Jumping", stateMachine)
    {
        _sm = (T3_MovementSM)stateMachine;
    }
    
    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.blue;
        
        Vector2 vel = _sm.rb2d.velocity;
        vel.y = _sm.jumpForce;
        _sm.rb2d.velocity = vel;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_isGrounded)
            stateMachine.ChangeState(_sm.idleState);
        
    }
    
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _isGrounded = _sm.rb2d.velocity.y < Mathf.Epsilon && _sm.rb2d.IsTouchingLayers(_groundlayer);
    }
}