using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class T2_Chaser : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    [SerializeField] private Transform target = null;

    //Initial position variable 
    private Vector3 _initialPosition;
    private bool _isCanChase = false;

    private void Start()
    {
        _initialPosition = transform.position;
        if (agent == null)
        {
            if (!TryGetComponent(out agent))
                Debug.LogError("NavMeshAgent component not found.");
        }
    }

    private void Update()
    {
        if (target)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        if (_isCanChase)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
        else
        {
            agent.SetDestination(_initialPosition);
            agent.isStopped = false;
        }
    }
    
    public void SetCanChase(bool canChase)
    {
        _isCanChase = canChase;
    }
}