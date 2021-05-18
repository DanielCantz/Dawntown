using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: stops attacking 
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Decisions/StopMeleeAttackDecision")]
public class StopMeleeAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.enemyStatHandler.target.transform.position) > controller.navMeshAgent.stoppingDistance)
        { 
            return true;
        }
        else
        {
            return false;
        }
    }
}
