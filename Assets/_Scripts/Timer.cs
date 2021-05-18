using UnityEngine;
using UnityEngine.Events;
///
/// Author: Samuel Müller: sm184
/// Description: timer component, that executes a delegate, when completed.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class Timer : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    public float Lifetime { get => _lifetime; set => _lifetime = value; }

    private float _currentLifetime;

    private bool _isActive;

    public bool HasExceded { get => !_isActive; }

    [SerializeField]
    private UnityEvent _onTimerEnd;

    private void Start()
    {
        _currentLifetime = _lifetime;
        _isActive = true;
    }

    void Update()
    {
        if (_isActive)
        {
            _currentLifetime -= Time.deltaTime;
            if (_currentLifetime <= 0)
            {
                if (_onTimerEnd != null)
                {
                    _onTimerEnd.Invoke();
                    _isActive = false;
                }
            }

        }
    }

    public void Restart()
    {
        _currentLifetime = _lifetime;
        _isActive = true;
    }
}

