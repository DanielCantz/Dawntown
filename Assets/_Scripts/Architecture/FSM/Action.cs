using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: An Abstract parent class that must be implemented by each action. 
/// ==============================================
/// Changelog: 
/// ==============================================
///
public abstract class Action : ScriptableObject
{
    public abstract void Act(StateController controller);
}
