using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: A Decision that return true if the player is in AggroRange
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Decisions/GetAggroDecision")]
public class GetAggroDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (Vector3.Distance(controller.enemyStatHandler.target.transform.position, controller.gameObject.transform.position) <= controller.enemyStats.aggroRange)
        {
            EnemyGameManager.Instance.aggroEnemys++;
            return true;
        }
        return false;
    }
}
