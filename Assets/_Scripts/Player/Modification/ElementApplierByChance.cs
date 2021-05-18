using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: Element applier for offence
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class ElementApplierByChance : ElementApplier
{

    private float _damage;
    public float Damage { get => _damage; set => _damage = value; }

    private void OnCollisionEnter(Collision collision)
    {

        ApplyDebuff(collision.collider);
        Destroy(gameObject);
    }

    protected override void OnSuccess(Collider collider)
    {
        StatHandler enemyStatHandler = gameObject.GetComponent<StatHandler>();
        enemyStatHandler.TakeDamage(_damage);
    }

    protected override bool GetApplyCondition(Collider collider)
    {
        float randomNumber = Random.Range(0.0f, 1.0f);
        return base.GetApplyCondition(collider) && randomNumber < CurrentElement.Chance;
    }
}
