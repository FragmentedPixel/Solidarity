using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : iEnemyState
{
	public ChaseState(EnemyController enemyController) : base(enemyController)
	{
		
	}

	public override void StateUpdate ()
	{
        if (controller.target)
            Chase();
        else if (!controller.SearchForTargets()) //No other targets in range => Go to patrol.
            ToPatrolState();
	}

    #region Methods
    private void Chase()
    {
        controller.agent.Resume();
        controller.agent.SetDestination(controller.target.position);
        controller.LookAtTarget();

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);

        if (distance < controller.attackRange)
            ToAttackState();
        if (distance > controller.sightRange)
            ToPatrolState();
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    #endregion

    #region Trnasitions

    public override void ToPatrolState ()
	{
		controller.currentState = controller.patrolState;
	}

	public override void ToChaseState()
	{
		Debug.LogWarning("Can't transition to same state");
	}

	public override void ToAttackState()
	{
		controller.currentState = controller.attackState;
	}

	#endregion
}
