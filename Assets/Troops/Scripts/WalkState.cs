using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : iTroopState
{
    public WalkState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {
        WalkToDestination();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
        EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints>();
        if(enemyHP != null)
        {
            controller.target = enemyHP.transform;

			if (controller.DistanceToTarget() > controller.fightRange)
				ToAggroState ();
			else
				ToFightState ();
        }
    }
    private void WalkToDestination()
    {
		controller.agent.SetDestination (controller.destination);
		controller.agent.Resume ();

		if (controller.agent.remainingDistance < controller.agent.stoppingDistance && !controller.agent.pathPending)
			ToIdleState ();
    }
    #endregion

    #region Transitions
	public override void ToIdleState()
	{
		controller.currentState = controller.idleState;
	}

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
		//Already in this state.
    }
    #endregion

}
