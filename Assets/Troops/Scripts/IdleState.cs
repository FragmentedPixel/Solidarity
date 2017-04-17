using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : iTroopState
{
    public IdleState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {
		
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
		EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints> ();
		if (enemyHP != null) 
		{
			controller.target = enemyHP.transform;

			if (controller.DistanceToTarget() > controller.fightRange)
				ToAggroState ();
			else
				ToFightState ();
		}
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
		controller.currentState = controller.walkState;
    }
    #endregion

}
