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
			Chase ();
		else
			controller.SearchForTargets ();
	}

    #region Methods
    private void Chase()
    {
        controller.agent.Resume();
        controller.agent.SetDestination(controller.target.position);
		controller.anim.SetBool ("Walking", true);
        controller.LookAtTarget();

		if (controller.DistanceToTarget() < controller.attackRange)
            ToAttackState();
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
		controller.currentState = controller.chaseState;
	}

	public override void ToAttackState()
	{
		controller.currentState = controller.attackState;
	}

	#endregion
}
