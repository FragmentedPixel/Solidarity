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
		if (controller.wayPoints.Count > 0)
			ToWalkState ();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
		EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints> ();
		if (enemyHP != null) 
		{
			controller.target = enemyHP.transform;
			float distance = Vector3.Distance (controller.transform.position, controller.target.position);
			if (distance > controller.fightRange)
				ToAggroState ();
			else
				ToFightState ();
		}
    }
    #endregion

    #region Transitions
    public override void ToFightState()
    {
        
    }

    public override void ToAggroState()
    {

    }

    public override void ToWalkState()
    {

    }
    #endregion

}
