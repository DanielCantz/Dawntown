using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: A container class that returns a true or false state for the state controller for each transition depending on what the decision returned. 
/// ==============================================
/// Changelog: 
/// ==============================================
///
[System.Serializable]
public class Transition
{
    public Decision decision;
    public State trueState;
    public State falseState;
}
