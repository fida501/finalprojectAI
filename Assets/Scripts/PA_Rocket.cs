using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PA_Rocket : MonoBehaviour
{
    public Sprite rockSprite;
    public AIDestinationSetter destinationSetter;
    public bool isChasing = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("T5_Player"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = rockSprite;
            destinationSetter.enabled = true;
            isChasing = true;
        }
        
        if (other.CompareTag("Enemy") && isChasing)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
