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
[CreateAssetMenu(menuName = "PluggableAI/Decisions/LooseAggroDecision")]
public class LooseAggroDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (Vector3.Distance(controller.enemyStatHandler.target.transform.position, controller.gameObject.transform.position) >= controller.enemyStats.looseAggroRange)
        {
            return true;
        }
            
        return false;
    }
}
