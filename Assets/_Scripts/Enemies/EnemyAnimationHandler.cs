using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///
/// Author: Fred Akdogan
/// Description: Class that controls enemy Animators
/// ==============================================
/// Changelog:
/// ==============================================
public class EnemyAnimationHandler : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetFloat("horizontal", navMeshAgent.velocity.x);
        animator.SetFloat("vertical", navMeshAgent.velocity.z);

        if (navMeshAgent.velocity.x != 0 || navMeshAgent.velocity.z != 0)
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void attackAgentAnimation()
    {
        animator.SetTrigger("attack");
    }
}
