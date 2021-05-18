using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Rotate Enemy to Player instant
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Actions/RotateInstant")]
public class RotateInstant : Action
{
    public override void Act(StateController controller)
    {
        controller.transform.LookAt(new Vector3(controller.enemyStatHandler.target.transform.position.x, controller.transform.position.y, controller.enemyStatHandler.target.transform.position.z));
    }
}
