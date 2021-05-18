using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: makes shure the related game object looks always into the main camera.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class Billboard : MonoBehaviour
{
    private Transform _relatedCamera;
    void Start()
    {
        _relatedCamera = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + _relatedCamera.forward);   
    }
}
