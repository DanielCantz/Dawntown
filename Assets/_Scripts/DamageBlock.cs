using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        StatHandler otherStats = other.GetComponent<StatHandler>();
        if (otherStats)
        {
            otherStats.TakeDamage(10f);
            Debug.Log(otherStats.Health);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
        StatHandler otherStats = other.GetComponent<StatHandler>();
        if (otherStats)
        {
            otherStats.TakeDamage(0.5f);
            Debug.Log(otherStats.Health);
        }
    }
}
