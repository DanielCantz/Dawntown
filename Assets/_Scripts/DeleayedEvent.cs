using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeleayedEvent : MonoBehaviour
{
    [SerializeField]
    private float _triggerTime;
    [SerializeField]
    private UnityEvent _onDelay;

    private void Start()
    {
        Invoke("Trigger", _triggerTime);
    }

    private void OnEnable()
    {
        Invoke("Trigger", _triggerTime);
    }

    private void Trigger()
    {
        if (_onDelay != null)
        {
            _onDelay.Invoke();
        }
    }
}
