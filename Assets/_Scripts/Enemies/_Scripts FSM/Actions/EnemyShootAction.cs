using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Shoot Projectiles
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/EnemyShootAction")]
public class EnemyShootAction : Action
{
    public override void Act(StateController controller)
    {
        if (controller.enemyStatHandler.currentBulletAmount < controller.enemyStats.bulletAmount)
        {
            if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
            {
                controller.enemyStatHandler.currentBulletAmount++;
                shoot(controller);
            }
        }
        else
        {
            if (controller.CheckIfCountDownElapsed(controller.enemyStats.reloadTime))
                controller.enemyStatHandler.currentBulletAmount = 0;
        }
    }

    private void shoot(StateController controller)
    {
        controller.enemyAnimationHandler.attackAgentAnimation();
        GameObject fPoint = controller.GetComponent<EnemyStatHandler>().FirePoint;

        var randomNumberX = UnityEngine.Random.Range(-controller.enemyStats.shootingInaccuracy, controller.enemyStats.shootingInaccuracy);
        var randomNumberZ = UnityEngine.Random.Range(-controller.enemyStats.shootingInaccuracy, controller.enemyStats.shootingInaccuracy);
        var randomNumberY = UnityEngine.Random.Range(-controller.enemyStats.shootingInaccuracy, controller.enemyStats.shootingInaccuracy);

        var t = Instantiate(controller.enemyStats.Weapon, fPoint.transform.position, fPoint.transform.rotation) as GameObject;
        EnemyProjectile eP = t.GetComponent<EnemyProjectile>();
        eP.element = controller.enemyStats.element;
        eP.firePoint = controller.enemyStatHandler.FirePoint;
        eP.damage = controller.enemyStats.damage;
        eP.duration = controller.enemyStats.duration;
        eP.slowAmount = controller.enemyStats.slowAmount;
        eP.startProjectile();
        eP.speedBullet();

        fPoint.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        Destroy(t, 5);
    }
}
