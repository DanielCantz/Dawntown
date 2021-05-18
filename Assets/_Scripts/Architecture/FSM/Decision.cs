using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: An Abstract parent class that must be implemented by each decision. 
/// ==============================================
/// Changelog: 
/// ==============================================
///
public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}
