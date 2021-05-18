using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform SwordAttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float SwordForceMultiplier;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Play Attack animation
        animator.SetTrigger("SwordAttack");

        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(SwordAttackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * SwordForceMultiplier);
            Debug.Log("Hit" + enemy.name);
        }

        Debug.Log("SwordAttack");
    }

    //Show attackRange
    void OnDrawGizmosSelected()
    {
        if (SwordAttackPoint == null)
            return;
        Gizmos.DrawWireSphere(SwordAttackPoint.position, attackRange);
      
    }

}
