using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_StateMachine : MonoBehaviour
{
    T3_BaseState currentState;

    private void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(T3_BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    protected virtual T3_BaseState GetInitialState()
    {
        return null;
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "No state";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}