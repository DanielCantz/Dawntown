using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMap : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Something fell out of the map");
        if (other.gameObject.CompareTag("Player"))
        {
            StatHandler statHandler = other.gameObject.GetComponent<StatHandler>();
            statHandler.TakeDamage(9999);
        }
        else
        {
            GameObject.Destroy(other.gameObject);
        }
            
    }
}
