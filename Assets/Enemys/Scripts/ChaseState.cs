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
        Chase();
        Look();

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);

        if (distance < controller.attackRange)
            ToAttackState();
        if (distance > controller.sightRange)
            ToPatrolState();
	}

    #region Methods

    private void Look()
    {
        Vector3 lookPoint = new Vector3(controller.target.position.x, controller.transform.position.y, controller.target.position.z);
        controller.transform.LookAt(lookPoint);
    }

    private void Chase()
    {
        controller.agent.Resume();
        controller.agent.SetDestination(controller.target.position);
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
