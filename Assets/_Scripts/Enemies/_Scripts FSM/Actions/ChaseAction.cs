using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Chase the enemy
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/ChaseAction")]
public class ChaseAction : Action
{ 
    public override void Act(StateController controller)
    {
        chase(controller);
    }

    private void chase(StateController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.enemyStatHandler.target.transform.position) > controller.navMeshAgent.stoppingDistance)
            controller.navMeshAgent.SetDestination(controller.enemyStatHandler.target.transform.position);
    }
}
