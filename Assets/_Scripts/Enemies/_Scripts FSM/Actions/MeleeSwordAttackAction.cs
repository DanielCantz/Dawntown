using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Melee Attack - Rotate Sword
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/MeleeSwordAttackAction")]
public class MeleeSwordAttackAction : Action
{
    public override void Act(StateController controller)
    {
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
        {
            controller.enemyStatHandler.sword.GetComponent<Sword>().animator.SetTrigger("attack");
            controller.enemyAnimationHandler.attackAgentAnimation();
        }
            
    }
}
