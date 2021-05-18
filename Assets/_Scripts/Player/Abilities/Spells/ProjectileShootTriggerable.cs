using UnityEngine;
using System.Collections;
///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: triggers offensive attack
/// ==============================================
/// Changelog:
/// Samuel Müller - refactorings & tier implementation
/// ==============================================
///

public class ProjectileShootTriggerable
{
    private ProjectileSpell m_spell;
    private Vector3 point;
    private Camera cam;
    private Plane plane = new Plane(new Vector3(0,0,0), new Vector3(1,0,0),new Vector3(0,0,1));

    public ProjectileShootTriggerable(ProjectileSpell spell)
    {
        m_spell = spell;
    }

    public void Launch()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pointOnPlane;
        float enter;
        float damage;
        float lifedrain;

        if (plane.Raycast(ray, out enter))
        {
            switch (m_spell.WeaponMod)
            {
                case WeaponEnum.blade:
                    m_spell.Projectile.transform.localScale += Vector3.right * m_spell.Scale;
                    break;
                case WeaponEnum.magiccore:
                    m_spell.Projectile.transform.localScale =  Vector3.one * m_spell.Scale;
                    break;
                case WeaponEnum.barrel:
                    m_spell.Projectile.transform.localScale = Vector3.one* m_spell.BaseScale;
                    break;
            }
            
            pointOnPlane = ray.GetPoint(enter);

            Vector3 positionWithoutY = new Vector3(m_spell.BulletSpawn.position.x, 0, m_spell.BulletSpawn.position.z);
            Vector3 bulletVector = pointOnPlane - positionWithoutY;
            m_spell.DirectionVector = bulletVector;

            m_spell.ResetSpell();

            AbstractWildcard currentWilcard = AbilityHandler.Instance.Attack.Wildcard;
            currentWilcard.TriggerWildcardAttack(m_spell.WeaponMod);

            Rigidbody clonedBullet = Object.Instantiate(m_spell.Projectile, m_spell.BulletSpawn.position, m_spell.BulletSpawn.rotation) as Rigidbody;

            clonedBullet.GetComponent<ApplyElementChance>().CurrentElement = AbilityHandler.Instance.Attack.Element;

            damage = m_spell.Damage + m_spell.Damage * Random.Range(-m_spell.DamageRange, m_spell.DamageRange);
            lifedrain = m_spell.Lifedrain;
            clonedBullet.GetComponent<ApplyElementChance>().Lifedrain = lifedrain;

            if (Random.Range(0,1f) <= m_spell.CritChance)
            {
                clonedBullet.GetComponent<ApplyElementChance>().Damage = damage * 2;
            }
            else
            {
                clonedBullet.GetComponent<ApplyElementChance>().Damage = damage;
            }

            if (m_spell.Lifetime == 0)
            {
                RangedBullet bullet = clonedBullet.gameObject.AddComponent<RangedBullet>();
                bullet.Range = m_spell.Range;
            } else
            {
                TimedBullet bullet = clonedBullet.gameObject.AddComponent<TimedBullet>();
                bullet.Lifetime = m_spell.Lifetime;
            }

            switch (m_spell.WeaponMod)
            {
                case WeaponEnum.blade:
                    clonedBullet.GetComponent<SwordStrike>().BladeAttack(clonedBullet, m_spell.Scale, m_spell.ProjectileSpread, m_spell.BaseLifetime, m_spell.BulletSpawn.position + bulletVector.normalized);
                    break;
                case WeaponEnum.magiccore:
                    clonedBullet.AddForce(m_spell.DirectionVector.normalized * m_spell.ProjectileForce);
                    break;
                case WeaponEnum.barrel:
                    float spreadValue = m_spell.ProjectileSpread / 180;
                    Vector3 strayVector = new Vector3(UnityEngine.Random.Range(-spreadValue, spreadValue), 0,UnityEngine.Random.Range(-spreadValue, spreadValue));
                    clonedBullet.AddForce((bulletVector.normalized + strayVector) * m_spell.ProjectileForce);
                    break;
            }
        }
    }
}