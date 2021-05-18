using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Rotate Enemy to Player smooth
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/RotateSmoothToPlayerAction")]
public class RotateSmoothToPlayerAction : Action
{
    public override void Act(StateController controller)
    {
        var targetRotation = Quaternion.LookRotation(controller.enemyStatHandler.target.transform.position - controller.transform.position, Vector3.forward);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, Time.deltaTime * controller.enemyStats.rotationSpeed);
    }
}
