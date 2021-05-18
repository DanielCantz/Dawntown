using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Projectile Script
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class EnemyProjectile : MonoBehaviour
{

    public float forceMin;
    public float forceMax;
    [HideInInspector] public ElementEnum element;
    [HideInInspector] public GameObject firePoint;
    [HideInInspector] public float damage;
    [HideInInspector] public float slowAmount;
    [HideInInspector] public float duration;
    private float randomForce;
    public Rigidbody rb;
    [SerializeField] private ParticleSystem particleSystem;

    private float zigzagForce = 500;
    private bool waitLightning = false;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Partilce hit");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Partilce hit player");
            StatHandler playerStatHandler = other.GetComponent<StatHandler>();
            Slow slow = new Slow(playerStatHandler, new Stat(duration), new ScalingStatModificator(slowAmount));
            other.GetComponent<BuffHandler>().AddBuff(slow);
        }
    }

    private void Update()
    {
        if (element == ElementEnum.fire)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            transform.position += new Vector3(0f, 0.001f, 0f);
        }
        else if (element == ElementEnum.lightning)
        {
            StartCoroutine(waitZigzag());
            if (waitLightning)
            {
                StartCoroutine(zigzag());
            }
        }
    }

    IEnumerator zigzag()
    {
        rb.AddForce(new Vector3(zigzagForce, 0, 0));
        zigzagForce *= -1;

        yield return new WaitForSeconds(1);
    }
    IEnumerator waitZigzag()
    {
        if (!waitLightning)
        {
            yield return new WaitForSeconds(.5f);
            waitLightning = true;
        }
    }

    public void startProjectile()
    {
        if (element == ElementEnum.ice)
            particleSystem.Play();
        else
            particleSystem.Stop();


        if (forceMin >= forceMax)
            randomForce = UnityEngine.Random.Range(forceMax, forceMin + 0.01f);
        else
            randomForce = UnityEngine.Random.Range(forceMin, forceMax);

        speedBullet();
    }

    public void speedBullet()
    {
        if (firePoint != null)
            rb.AddForce(firePoint.transform.forward * randomForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreLayerCollision(12, 8);

        GameObject other = collision.gameObject;

        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                BuffHandler playerBuffHandler = other.GetComponent<BuffHandler>();
                StatHandler statHandler = other.GetComponent<StatHandler>();
                Buff debuff;

                switch (element)
                {
                    case ElementEnum.fire:
                        debuff = new Burn(statHandler, new Stat(duration), damage);
                        playerBuffHandler.AddBuff(debuff);
                        break;
                    case ElementEnum.ice:
                        debuff = new Slow(statHandler, new Stat(duration), new ScalingStatModificator(slowAmount));
                        statHandler.TakeDamage(damage);
                        playerBuffHandler.AddBuff(debuff);
                        break;
                    case ElementEnum.lightning:
                        debuff = new Stun(statHandler, new Stat(duration));
                        statHandler.TakeDamage(damage);
                        playerBuffHandler.AddBuff(debuff);
                        break;
                }
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Untagged"))
        {
            // Debug.Log("Projectile collide with ELSE: " + other.gameObject.name);
            Destroy(gameObject);
        }
        else
        {
            // Debug.Log("Projectile collide with: " + other.gameObject.name);
        }
    }
}

