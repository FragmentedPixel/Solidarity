using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : iTroopState
{
    private int index = -1;

    public WalkState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {
        Walk();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
        EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints>();
        if(enemyHP != null)
        {
            controller.target = enemyHP.transform;
            ToAggroState();
        }
    }

    private void Walk()
    {
        if(controller.agent.remainingDistance < controller.agent.stoppingDistance && !controller.agent.pathPending)
        {
            controller.agent.Resume();
            index = (index + 1) % controller.wayPoints.Length;
            controller.agent.SetDestination(controller.wayPoints[index]);
        }
    }
    #endregion

    #region Transitions
    public override void ToFightState()
    {
        controller.currentState = controller.fightState;
    }

    public override void ToAggroState()
    {
        controller.currentState = controller.aggroState;
    }

    public override void ToWalkState()
    {
        Debug.LogWarning("Can't transition to same state");
    }
    #endregion

}
