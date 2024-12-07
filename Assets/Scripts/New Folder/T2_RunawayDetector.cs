using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_RunawayDetector : MonoBehaviour
{
    [SerializeField] private T2_Chaser _chaser;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("T2_Dikejar"))
        {
            _chaser.SetCanChase(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("T2_Dikejar"))
        {
            _chaser.SetCanChase(false);
        }
    }
}