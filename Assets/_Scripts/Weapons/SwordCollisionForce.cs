using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionForce : MonoBehaviour
{
    public float SwordForceMultiplier;
    public float SwordForceEnemyMultiplier;
    public float damage = 10;
    public LayerMask enemyLayers;

    void OnTriggerEnter(Collider other)  
    {
      /*   Debug.Log("Hit" + other.gameObject.name);
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * SwordForceMultiplier);
        }*/
        
        // Damage if enemy
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.transform.position += (other.gameObject.transform.position - transform.parent.position).normalized * SwordForceEnemyMultiplier;
            StatHandler sh = other.gameObject.GetComponent<StatHandler>();
            if (sh != null)
            {
                sh.TakeDamage(damage); 
            }
            else
            {
                // Debug.Log("F");
            }

            // Debug.Log("Damage" + other.gameObject.name);
        }
    }
}
