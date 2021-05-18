using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Central point of every opponent. 
/// The state controller tells which state is currently active and what the opponent should act upon. 
/// This is determined by the decision and the resulting states. 
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;
    public EnemyStats enemyStats;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public EnemyStatHandler enemyStatHandler;
    [HideInInspector] public EnemyAnimationHandler enemyAnimationHandler;

    [HideInInspector] public bool aiActive = true;

    private void Start()
    {
        enemyStatHandler.MaxHealth = enemyStats.StartingHealth;
        enemyStatHandler.Element = enemyStats.element;
    }

    // Start is called before the first frame update
    void Awake()
    {
        enemyStatHandler = GetComponent<EnemyStatHandler>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    /// <summary>
    /// Change between the new state. 
    /// </summary>
    /// <param name="nextState">New Current State</param>
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    /// <summary>
    /// A timer that the ScriptableObjects can use to count for each opponent individually. 
    /// Since the ScriptableObjects are not monobehaviour and prefabs this would count for each opponent at the same time. 
    /// </summary>
    /// <param name="duration">duration unitl it returns true</param>
    /// <returns></returns>
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        if (stateTimeElapsed >= duration)
        {
            stateTimeElapsed = 0;
            return true;
        } else
            return false;
    }

    /// <summary>
    /// A method that is executed after a state is left. 
    /// </summary>
    private void OnExitState()
    {

    }

    void OnDrawGizmos()
    {
        
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(this.transform.position, enemyStats.aggroRange);
            Gizmos.color = Color.cyan;
            //if (enemyStatHandler.IdleArea != null)
            //{
            //    Gizmos.DrawWireSphere(enemyStatHandler.IdleArea, enemyStats.IdleAreaSize);
            //}
        }
    }
}