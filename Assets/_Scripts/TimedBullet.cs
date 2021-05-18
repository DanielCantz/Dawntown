using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: component on world transform objects. They're destroyed after a certain amount of time.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class TimedBullet : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    public float Lifetime { get => _lifetime; set => _lifetime = value; }

    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>().IsInvinsible)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>().IsInvinsible = false;
                Debug.Log("<color=green>WasThatThere Player invincible timeout</color>");
            }
            Destroy(gameObject);
        }

    }
}
