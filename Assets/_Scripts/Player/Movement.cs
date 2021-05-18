using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// Author: Samuel Müller, sm184
/// Description: Moves the player based on rigidbody and player inputs.
/// ==============================================
/// Changelog: 1.7.20 - Merlin Tisler - removed dashing in mouse direction and set it in movement direction
/// ==============================================
[RequireComponent(typeof(Rigidbody), typeof(StatHandler))]
public class Movement : MonoBehaviour
{
    private StatHandler _statHandler;
    private Rigidbody _rigidbody;
    private Vector3 _dashDirection = Vector3.forward;

    public Vector3 Direction { get => _dashDirection; }
    [SerializeField] private bool _isDashing = false;
    [SerializeField]  private BasicStatModifier _additionalDashSpeed;

    public void StartDashing()
    {
        _isDashing = true;
        _statHandler.Speed.AddMod(_additionalDashSpeed);
    }

    public void EndDashing()
    {
        _isDashing = false;
        _statHandler.Speed.RemoveMod(_additionalDashSpeed);
    }


    private void Start()
    {
        _statHandler = GetComponent<StatHandler>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            Vector3 newPosition = transform.position + _dashDirection * _statHandler.Speed.GetValue() * Time.deltaTime;
            _rigidbody.MovePosition(newPosition);
        }
        else
        {
            Vector3 direction = (Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right).normalized;
            if (direction != Vector3.zero)
            {
                _dashDirection = direction;
            }
            Vector3 oldPosition = transform.position;
            Vector3 newPosition = oldPosition + direction * _statHandler.Speed.GetValue() * Time.deltaTime;
            _rigidbody.MovePosition(newPosition);
        }
    }
    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = 0;
            return hitPoint;
        }
        return Vector3.zero;
    }
}