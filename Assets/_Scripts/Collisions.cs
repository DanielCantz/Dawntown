using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "placeholder")
        {
            Debug.Log("STOP" + other.gameObject.name);
        }
    }
}
