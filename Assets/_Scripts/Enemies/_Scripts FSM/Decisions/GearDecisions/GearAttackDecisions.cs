using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: If the Player is far away from the Player the it returns false
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Decisions/GearAttackDecisions")]
public class GearAttackDecisions : Decision
{
    public override bool Decide(StateController controller)
    {
        if (Vector3.Distance(controller.gameObject.transform.position, controller.enemyStatHandler.target.transform.position) <= controller.enemyStats.attackRange)
            return true;
        return false;
    }
}
