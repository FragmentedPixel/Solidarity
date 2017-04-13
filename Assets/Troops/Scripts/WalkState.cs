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
        Walk();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
        EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints>();
        if(enemyHP != null)
        {
            controller.target = enemyHP.transform;
			float distance = Vector3.Distance (controller.transform.position, controller.target.position);
			if (distance > controller.fightRange)
				ToAggroState ();
			else
				ToFightState ();
        }
    }

    private void Walk()
    {
		controller.agent.Resume ();
		controller.agent.SetDestination (controller.wayPoints[0]);
        
		if(controller.agent.remainingDistance < controller.agent.stoppingDistance && !controller.agent.pathPending)
		{
			controller.agent.Resume ();
			controller.wayPoints.Remove (controller.wayPoints[0]);

			if (controller.wayPoints.Count == 0)
				ToIdleState ();
        }
    }
    #endregion

    #region Transitions
	private void ToIdleState()
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
        Debug.LogWarning("Can't transition to same state");
    }
    #endregion

}
