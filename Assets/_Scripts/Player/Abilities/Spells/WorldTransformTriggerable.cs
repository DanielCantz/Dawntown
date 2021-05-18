//using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SocialPlatforms;

///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: triggers defensive spell.
/// ==============================================
/// Changelog:
/// Samuel Müller, sm184 - refactorings & tier implementation
/// ==============================================
///
public class WorldTransformTriggerable
{
    private WorldTransformSpell _spell;

    
    private Plane plane = new Plane(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1));
    
    private bool isGrounded;
    private int jumpForce;
    private int speed;

    public WorldTransformTriggerable(WorldTransformSpell spell)
    {
        _spell = spell;
    }

    public void Launch()
    {
        //Cast a ray on plane to get the world position
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pointOnPlane;
        float enter;

        if (plane.Raycast(ray, out enter))
        {
            //Get world position from ray
            pointOnPlane = ray.GetPoint(enter);

            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            _spell.ResetSpell();

            // Get wildcard and apply effect
            AbstractWildcard currentWildcard = AbilityHandler.Instance.Defense.Wildcard;
            currentWildcard.TriggerWildcardDefense(_spell.WeaponMod);

            //Calculate shoot direction vector
            Vector3 positionWithoutY = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
            Vector3 objectVector = pointOnPlane - positionWithoutY;
            
            GameObject clone;
            ApplyElement applyElement;

            switch (_spell.WeaponMod)
            {
                case WeaponEnum.blade:
                    Vector3 dashEndPosition = objectVector.normalized * _spell.Range;
                    clone = GameObject.Instantiate(_spell.WorldObject, dashEndPosition + new Vector3(Random.Range(-_spell.PlacementOffset, _spell.PlacementOffset), 0.05f, Random.Range(-_spell.PlacementOffset, _spell.PlacementOffset)), playerTransform.rotation);
                    clone.GetComponent<SwordDefense>().Dash(_spell.Scale, _spell.Lifetime, _spell.Range);
                    break;

                case WeaponEnum.magiccore:

                    RaycastHit hit;
                    Vector3 raycastPoint = new Vector3(playerTransform.position.x, 3, playerTransform.position.z);
                    
                    //Change this if the house/river prefab layer is adjusted
                    int world = 1 << 0;

                    Vector3 wallPosition;
                    // Check if wall would be in wall
                    if (Physics.Raycast(raycastPoint, objectVector.normalized, out hit, _spell.Range, world))
                    {
                        wallPosition  = playerTransform.position + objectVector.normalized * (hit.distance - 0.1f);
                    }
                    else
                    {
                      wallPosition  = playerTransform.position + objectVector.normalized * _spell.Range;
                    }
                    //Instantiate a copy of our Object
                    clone = GameObject.Instantiate(_spell.WorldObject, wallPosition + new Vector3(Random.Range(-_spell.PlacementOffset, _spell.PlacementOffset), _spell.WorldObject.transform.localPosition.y, Random.Range(-_spell.PlacementOffset, _spell.PlacementOffset)), playerTransform.rotation) as GameObject;
                    Vector3 localScale = clone.transform.localScale;
                    localScale.x *= _spell.Scale;
                    localScale.z *= _spell.Scale;
                    clone.transform.localScale = localScale;
                    applyElement = clone.GetComponentInChildren<ApplyElement>();
                    if (applyElement != null)
                    {
                        Element element = AbilityHandler.Instance.Defense.Element;
                        applyElement.CurrentElement = AbilityHandler.Instance.Defense.Element;
                    }

                    //Position Wall in player direction
                    clone.transform.forward = (wallPosition - playerTransform.position).normalized;
                    
                    //Start a timer to destroy bullet when there is no enemy or wall hit
                    TimedBullet bullet = clone.gameObject.AddComponent<TimedBullet>();
                    bullet.Lifetime = _spell.Lifetime;
                    break;

                case WeaponEnum.barrel:
                 
                    Vector3 puddlePosition = playerTransform.position;

                    //Instantiate a copy of our Object
                    clone = GameObject.Instantiate(_spell.WorldObject, puddlePosition + new Vector3(Random.Range(-_spell.PlacementOffset, _spell.PlacementOffset), 0.022f, Random.Range(-_spell.PlacementOffset, _spell.PlacementOffset)),playerTransform.rotation) as GameObject;
                    clone.transform.localScale *= _spell.Scale;
                    applyElement = clone.GetComponent<ApplyElement>();
                    if (applyElement != null)
                    {
                        applyElement.CurrentElement = AbilityHandler.Instance.Defense.Element;
                    }

                    //Position puddle at player position
                    clone.transform.forward = (puddlePosition - playerTransform.position).normalized;

                    //Start a timer to destroy object 
                    TimedBullet timer = clone.gameObject.AddComponent<TimedBullet>();
                    timer.Lifetime = _spell.Lifetime;
                    
                    Vector3 jumpPosition = objectVector.normalized * _spell.Range;

                    //Move Player to jumpPosition                  
                    GameObject player = GameObject.FindWithTag("Player");
                    player.GetComponent<BarrelDefense>().Jumping(jumpPosition, _spell.aBaseCoolDown, _spell.BaseLifetime);
                    break;
            }
        }
    }
}

