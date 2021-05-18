using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("<color = cyan>" + "BULLET HIT TAG: " + collision.gameObject.tag + "\nName: " + collision.transform);

        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("<color = yellow>" + "BULLET HIT TAG: " + other.gameObject.tag + "\nName: " + other.transform);

        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
