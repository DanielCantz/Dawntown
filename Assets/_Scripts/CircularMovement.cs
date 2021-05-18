using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement: MonoBehaviour
{
    private Boolean isStarted = false;
    private void Update()
    {
        if (isStarted)
        {
            transform.RotateAround(GameObject.FindWithTag("Player").transform.position,new Vector3(0,1,0),180 * Time.deltaTime);   
        }
    }

    public void startRotation()
    {
        isStarted = true;
    }
}
