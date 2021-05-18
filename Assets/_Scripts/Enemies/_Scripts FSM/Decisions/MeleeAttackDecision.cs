using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: if the player distance is under stopping discance - return true
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Decisions/MeleeAttackDecision")]
public class MeleeAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.navMeshAgent.remainingDistance < controller.navMeshAgent.stoppingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
