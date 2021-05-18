using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
///
/// Author: Merlin Tisler
/// Description: manage the dashing ability and spawning the element trail behind the player while dashing
/// ==============================================
/// Changelog:
/// 22.06.2020 - Samuel Müller - migrated player movement logic to the movement script & removed unnecessary code.
/// ==============================================
///
public class SwordDefense : MonoBehaviour
{
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _trailObjectDistance;
    [SerializeField] private GameObject _newTrailObject;
    [SerializeField] private float _trailObjectLifetime;
    [SerializeField] private float _trailObjectSize;
    private float _scale;
    private GameObject _player;

    public void Dash(float scale, float lifetime, float range)
    {
        _player = GameObject.FindWithTag("Player");
        _trailObjectLifetime = lifetime;
        _dashDuration = range;
        _scale = scale;
        var c = FindObjectsOfType<SwordDefense>();
        if (c.Length == 1)
        {
            _player.GetComponent<Movement>().StartDashing();
            StartCoroutine(Trail());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Trail()
    {
        // Debug.Log("start");
        Vector3 lastPos = _player.transform.position;

        Movement playerMovement = _player.GetComponent<Movement>();

        if (playerMovement.Direction == Vector3.zero)
        {
            Destroy(gameObject);
            yield break;
        }
        //set first TrailObject
        GameObject clone = Instantiate(_newTrailObject, lastPos, Quaternion.identity);
        clone.GetComponent<ApplyElement>().CurrentElement = _player.GetComponent<AbilityHandler>().Defense.Element;
        clone.GetComponent<ApplyElement>().CurrentElement = _player.GetComponent<AbilityHandler>().Defense.Element;
        clone.transform.localScale *= _scale;
        Destroy(clone, _trailObjectLifetime);
        

        //set TrailObjects every TrailObjectDistance for the dashDuration
        var i = 0f;
        while (i < _dashDuration)
        {
            if (Vector3.Distance(_player.transform.position, lastPos) >= _trailObjectDistance)
            {
                Vector3 spawnPos = lastPos + (_player.transform.position - lastPos).normalized * _trailObjectDistance;
                clone = Instantiate(_newTrailObject, spawnPos, Quaternion.identity);
                clone.GetComponent<ApplyElement>().CurrentElement = _player.GetComponent<AbilityHandler>().Defense.Element;
                clone.transform.localScale *= _scale;
                Destroy(clone, _trailObjectLifetime);
                lastPos = spawnPos;

            } 

            yield return new WaitForFixedUpdate();
            i += Time.fixedDeltaTime;
        }
        //stop dashing when dashDuration is over

        playerMovement.EndDashing();

        if (_player.GetComponent<StatHandler>().IsInvinsible)
        {
            _player.GetComponent<StatHandler>().IsInvinsible = false;
            Debug.Log("<color=green>WasThatThere Player invincible timeout</color>");
        }
        Destroy(gameObject);
    }

    private void InstantiateTrailObject(Vector3 position)
    {
        GameObject clone = Instantiate(_newTrailObject, position, Quaternion.identity);
        Destroy(clone, _trailObjectLifetime);
    }
}


