using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ApplyElementChance>() != null)
            other.GetComponent<ApplyElementChance>().Damage = other.GetComponent<ApplyElementChance>().Damage * 2;
    }
}
