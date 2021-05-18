using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBullet : MonoBehaviour
{
    [SerializeField] private float _range;

    public float Range { get => _range; set => _range = value; }

    private float _distance = 0f;
    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        Vector3 actualPosition = gameObject.transform.position;
        _distance = Vector3.Distance(actualPosition, _startingPosition);
        if (_distance >= _range)
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
