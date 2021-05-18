using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TransparencyManipulator : MonoBehaviour
{
    [SerializeField]
    private float _transparencyRate;

    private Image _image;
    
    [SerializeField]
    private bool _isIncteaseing;
    [SerializeField]
    private bool _isToggling = false;

    [SerializeField]
    private UnityEvent _transparencyEvent;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        Color color = _image.color;
        color.a = Mathf.Clamp(_isIncteaseing? color.a + Time.deltaTime * _transparencyRate : color.a - Time.deltaTime * _transparencyRate, 0, 255);
        if(color.a <= 0 || color.a >= 255)
        {
            if (_isToggling)
            {
                _isIncteaseing = !_isIncteaseing;
            } else
            {
                if (_transparencyEvent != null)
                {
                    _transparencyEvent.Invoke();
                }
                Destroy(this);
            }
        }
        _image.color = color;
    }
}
