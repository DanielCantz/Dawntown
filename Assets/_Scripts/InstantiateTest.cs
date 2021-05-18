using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public GameObject newObject;
    public Transform newPosition;
    // Start is called before the first frame update
    void Start()
    {

     
        
            Instantiate(newObject, newPosition.position, Quaternion.identity);
        
    }
}
