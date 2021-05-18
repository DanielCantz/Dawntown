using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Activate / Deactivate Shield in Attack rate time
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/ShieldAttackAction")]
public class ShieldAttackAction : Action
{
    public override void Act(StateController controller)
    {
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
        {
            if (controller.enemyStatHandler.shield.activeSelf == true)
            {
                controller.enemyStatHandler.shield.SetActive(false);
                controller.enemyAnimationHandler.attackAgentAnimation();
            }
            else
            {
                controller.enemyStatHandler.shield.SetActive(true);
                controller.enemyAnimationHandler.attackAgentAnimation();
            }
        }
    }
}
