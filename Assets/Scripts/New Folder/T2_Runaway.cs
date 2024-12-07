using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class T2_Runaway : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform chaser = null;
    [SerializeField] private float displacementDist = 5f;


    private void Start()
    {
        if (agent == null)
        {
            if (!TryGetComponent(out agent))
                Debug.LogError("NavMeshAgent component not found.");
        }
    }

    private void Update()
    {
        if (chaser == null)
        {
            return;
        }

        var chaserPos = chaser.position;
        var objectPos = transform.position;

        Vector3 normDir = (chaserPos - objectPos).normalized;
        normDir = Quaternion.AngleAxis((Random.Range(0, 179)), Vector3.up) * normDir;
        MoveToPos(objectPos - (normDir * displacementDist));
    }

    private void MoveToPos(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Vector3 direction = (chaserPos - objectPos).normalized;
        // float mag = direction.magnitude;
        // print(mag);
        // Gizmos.DrawLine(objectPos, chaserPos);
    }
}