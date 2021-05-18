using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: The Idle Action makes the enemy patrol his Idle Area between min and max waiting time which are set by the stats so that he doesn't patrol and move all the time on the Navmesh within this area
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/IdleAction")]
public class IdleAction : Action
{
    private bool wait = true;

    public override void Act(StateController controller)
    {
        if (controller.enemyStats.idleWalkAround)
            controller.StartCoroutine(isWalkingWhenIdle(controller));
    }

    /// <summary>
    /// When the opponent is close to his point he looks for the next one. Otherwise he runs. 
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    private IEnumerator isWalkingWhenIdle(StateController controller)
    {
        if (controller.navMeshAgent.remainingDistance < controller.navMeshAgent.stoppingDistance + 1f && wait)
        {
            if (wait)
            {
                yield return new WaitForSecondsRealtime(Random.Range(controller.enemyStats.idleChangePositionMin, controller.enemyStats.idleChangePositionMax));
                wait = false;
            }


        }
        else if (controller.navMeshAgent.remainingDistance < controller.navMeshAgent.stoppingDistance + 1f && !wait)
        {
            controller.navMeshAgent.SetDestination(isRandomWalkablePointInSphere(controller));
            wait = true;
        }
    }

    /// <summary>
    /// Returns a point on the Navmesh within the Idle Area. 
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    private Vector3 isRandomWalkablePointInSphere(StateController controller)
    {
        bool foundPoint = false;
        Vector3 res = new Vector3();

        while (!foundPoint)
        {
            res = controller.enemyStatHandler.IdleArea + Random.insideUnitSphere * controller.enemyStats.IdleAreaSize;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(res, out hit, 1.0f, NavMesh.AllAreas) && Vector3.Distance(res, controller.transform.position) > controller.enemyStats.IdleAreaSize / 4)
            {
                // Debug.Log("point found\nDistance: " + Vector3.Distance(res, controller.transform.position));
                foundPoint = true;
                return hit.position;
            }
        }
        return res;
    }
}
