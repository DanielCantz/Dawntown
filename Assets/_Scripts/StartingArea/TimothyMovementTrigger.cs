///
/// Author: Daniel Cantz
/// Description: Triggers Timmothys Movement in Starting area
/// ==============================================
/// Changelog: 
/// ==============================================
///

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimothyMovementTrigger : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float speed;

    public void TriggerMovement()
    {
        Debug.Log("Timothees Movement was triggered");
        StartCoroutine(TimothyMovement());
    }

    IEnumerator TimothyMovement()
    {
        while(Vector3.Distance(gameObject.transform.position, targetTransform.position) > 0.05f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetTransform.position, speed * Time.deltaTime);

            yield return null;
        }
    }
}
