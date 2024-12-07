using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public float sensorLength = 5f;
    public float speed = 10f;
    public float directionalValue = 1.0f;
    public float turnValue = 0.0f;
    public float turnSpeed = 50.0f;
    public Collider myCollider;

    private void Start()
    {
        myCollider = transform.GetComponent<Collider>();
    }

    private void Update()
    {
        RaycastHit hit;
        int flag = 0;
        // Right Sensor
        if (Physics.Raycast(transform.position, transform.right, out hit, (sensorLength + transform.localScale.x)))
        {
            if (!hit.collider.CompareTag("Obstacle") || hit.collider == myCollider)
            {
                return;
            }

            turnValue -= 1;
            flag++;
        }
        //Left Sensor
        else if (Physics.Raycast(transform.position, -transform.right, out hit,
                     (sensorLength + transform.localScale.x)))
        {
            if (!hit.collider.CompareTag("Obstacle") || hit.collider == myCollider)
            {
                return;
            }

            turnValue += 1;
            flag++;
        }
        // Front Sensor
        else if (Physics.Raycast(transform.position, transform.forward, out hit, (sensorLength + transform.localScale.z)))
        {
            if (!hit.collider.CompareTag("Obstacle") || hit.collider == myCollider)
            {
                return;
            }

            if (directionalValue == 1.0f)
            {
                directionalValue = -1f;
            }

            flag++;
        }
        //Back Sensor
        else if (Physics.Raycast(transform.position, -transform.forward, out hit,
                     (sensorLength + transform.localScale.z)))
        {
            if (!hit.collider.CompareTag("Obstacle") || hit.collider == myCollider)
            {
                return;
            }

            if (directionalValue == -1.0f)
            {
                directionalValue = 1f;
            }

            flag++;
        }

        if (flag == 0)
        {
            turnValue = 0;
        }

        transform.Rotate(Vector3.up, (turnValue * turnSpeed) * Time.deltaTime);
        transform.position += transform.forward * (speed * directionalValue) * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * (sensorLength + transform.localScale.z));
        Gizmos.DrawRay(transform.position, -transform.forward * (sensorLength + transform.localScale.z));
        Gizmos.DrawRay(transform.position, transform.right * (sensorLength + transform.localScale.x));
        Gizmos.DrawRay(transform.position, -transform.right * (sensorLength + transform.localScale.x));
    }
}

