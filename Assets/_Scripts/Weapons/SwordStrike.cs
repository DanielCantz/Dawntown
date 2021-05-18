using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

///
/// Author: Merlin Tisler
/// Description: manage the blade attack and the characteristics of the blade
/// ==============================================
/// Changelog:
/// ==============================================
///

public class SwordStrike : MonoBehaviour
{
    private float schwingSeconds;
    private float schwingArcDegrees;
    private float bladeLength;
    private Rigidbody swordObject;
    private GameObject player;
    private GameObject parentObject;

    IEnumerator Swing()
    {
        float degreesPerSecond = schwingArcDegrees / schwingSeconds;

        Rigidbody localSwordBody = swordObject;
        var i = 0.05f;
        while (i < schwingSeconds)
        {
            localSwordBody.transform.Rotate( Vector3.up, degreesPerSecond * Time.deltaTime, Space.Self);
            yield return null;
            i += Time.deltaTime;
            parentObject.transform.position = player.transform.position;
            // Debug.DrawLine(transform.parent.position, transform.parent.position+transform.parent.forward, Color.cyan);
        }
        Destroy(parentObject);
    }

    public void BladeAttack(Rigidbody spellBody, float length, float degrees, float lifetime, Vector3 spawn)
    {
        player = GameObject.FindWithTag ("PlayerBulletSpawn");
        parentObject = new GameObject();
        bladeLength = length;
        schwingArcDegrees = degrees;
        schwingSeconds = lifetime;
        swordObject = spellBody;
        parentObject.transform.position = player.transform.position;

        parentObject.transform.LookAt(spawn);
        
        swordObject.transform.SetParent(parentObject.transform);
        swordObject.transform.localScale = new Vector3(length, 1, 1);
        swordObject.transform.localPosition = Vector3.zero;
        swordObject.transform.localRotation = Quaternion.Euler(0, 90 - (schwingArcDegrees / 2f), 0);     
        StartCoroutine(Swing());
    }
}