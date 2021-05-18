using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: State is a container that controls actions, transitions and a StateColor (for debugging). 

/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.gray;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    /// <summary>
    /// This function calls each assigned action script and calls them one after another. 
    /// </summary>
    /// <param name="controller">StateController</param>
    private void DoActions(StateController controller)
    {
        foreach (var action in actions)
        {
            action.Act(controller);
        }
    }

    /// <summary>
    /// This function looks if a decision returns true or false and on this basis the state is changed. 
    /// </summary>
    /// <param name="controller">StateController</param>
    private void CheckTransitions(StateController controller)
    {
        foreach (var trans in transitions)
        {
                bool decisionSucceeded = trans.decision.Decide(controller);

                if (decisionSucceeded)
                {
                    controller.TransitionToState(trans.trueState);
                }
                else
                {
                    controller.TransitionToState(trans.falseState);
                }
        }
    }
}
