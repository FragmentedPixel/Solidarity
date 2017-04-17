using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : iEnemyState
{
    float currentTime = 0f;
	#region Initialzation
    public AttackState(EnemyController enemyController) : base(enemyController)
    {

    }
	#endregion

    public override void StateUpdate()
    {
		if (controller.target)
			AttackTarget ();
		else
			controller.SearchForTargets ();
    }

    #region Methods
    public abstract void Attack();

    public void AttackTarget()
    {
        controller.agent.Stop();
		controller.anim.SetBool ("Walking", false);
        controller.LookAtTarget();

        if (currentTime > controller.attackCooldown)
        {
            currentTime = 0f;
            Attack();
        }
        else
            currentTime += Time.deltaTime;

		if (controller.DistanceToTarget() > controller.attackRange)
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
		controller.currentState = controller.attackState;
	}

	#endregion
}
