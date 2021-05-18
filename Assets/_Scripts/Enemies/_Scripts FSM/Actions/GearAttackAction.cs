using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Throw Obstacle
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/GearAttackAction")]
public class GearAttackAction : Action
{
    public GameObject[] ThrowObstacles;
    public int projectileAmount;
    public float forceMin;
    public float forceMax;
    public float strayFactor;

    public override void Act(StateController controller)
    {
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
            shoot(controller);
    }

    private void shoot(StateController controller)
    {
        GameObject fPoint = controller.GetComponent<EnemyStatHandler>().FirePoint;

        for (int i = 0; i < projectileAmount; i++)
        {
           // fPoint.transform.rotation = Quaternion.Euler(0, 0, 0);

            var randomNumberX = UnityEngine.Random.Range(-strayFactor, strayFactor);
            var randomNumberZ = UnityEngine.Random.Range(-strayFactor, strayFactor);
            var randomNumberY = UnityEngine.Random.Range(-strayFactor, strayFactor);
            int check = UnityEngine.Random.Range(0, ThrowObstacles.Length);
            float randomForce;
            if (forceMin >= forceMax)
                randomForce = UnityEngine.Random.Range(forceMax, forceMin + 0.01f);
            else
                randomForce = UnityEngine.Random.Range(forceMin, forceMax);

            var t = Instantiate(ThrowObstacles[check], fPoint.transform.position, fPoint.transform.rotation) as GameObject;

            fPoint.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            t.GetComponent<Rigidbody>().AddForce(fPoint.transform.forward * randomForce);
            t.GetComponent<Rigidbody>().useGravity = true;
            Destroy(t, 5);
        }
    }
}
