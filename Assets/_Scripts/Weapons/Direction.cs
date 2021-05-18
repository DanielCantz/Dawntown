using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (Input.GetAxis("Horizontal") > 0)
        {
            offset = new Vector3(0.1f, 0, 0);
            transform.localRotation = Quaternion.Euler(0,90,0);
            //print("rechts");
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            offset = new Vector3(-0.1f, 0, 0);
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            //print("links");
        }
       else if (Input.GetAxis("Vertical") > 0)
        {
            offset = new Vector3(0, 0, 0.1f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //print("hoch");
        }
      else  if (Input.GetAxis("Vertical") < 0)
        {
            offset = new Vector3(0, 0, -0.1f);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            //print("runter");
        }
        transform.position += offset;
     
    }
}
