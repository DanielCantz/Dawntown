using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: simple script to rotate hud elements based on a float value.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class SpinWheel : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    void Update()
    {
        transform.Rotate(Vector3.back * _speed * 0.01f, Space.Self);
    }
}
