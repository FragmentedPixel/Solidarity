using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : iEnemyState
{
    float currentTime = 0f;

    public AttackState(EnemyController enemyController) : base(enemyController)
    {

    }

    public override void StateUpdate()
    {
        if (controller.target)
            FightTarget();
        else if (controller.SearchForTargets())
            ToChaseState();
        else
            ToPatrolState();
    }

    #region Methods
    public abstract void Attack();

    public void FightTarget()
    {
        controller.agent.Stop();
        controller.LookAtTarget();

        if (currentTime > controller.attackCooldown)
        {
            currentTime = 0f;
            Attack();
        }
        else
            currentTime += Time.deltaTime;

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);

        if (distance > controller.sightRange)
            ToPatrolState();
        else if (distance > controller.attackRange)
            ToChaseState();
    }

    public override void OnTriggerEnter(Collider other)
    {

    }    
    #endregion


    #region Trnasitions

    public override void ToPatrolState()
	{
		controller.currentState = controller.patrolState;
	}

	public override void ToChaseState()
	{
		controller.currentState = controller.chaseState;
	}

	public override void ToAttackState()
	{
		Debug.LogWarning("Can't transition to same state");
	}

	#endregion
}
