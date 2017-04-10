using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : iEnemyState
{
    float currentTime = 0f;

	public AttackState(EnemyController enemyController) : base(enemyController)
	{
		
	}

    public override void StateUpdate()
    {
        //TODO: Check if no enemies are nearby.
        if (!controller.target)
        {
            ToPatrolState();
            return;
        }

        Look();
        Attack();

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);

        if (distance > controller.sightRange)
            ToPatrolState();
        else if (distance > controller.attackRange)
            ToChaseState();
    }

    #region Methods

    private void Look()
    {
        Vector3 lookPoint = new Vector3(controller.target.position.x, controller.transform.position.y, controller.target.position.z);
        controller.transform.LookAt(lookPoint);
    }

    private void Attack()
    {
        controller.agent.Stop();

        if (currentTime > controller.attackCooldown)
        {
            Debug.Log("Player should attack");
            currentTime = 0f;
            TroopHitPoints troopHitPoints = controller.target.GetComponent<TroopHitPoints>();
            troopHitPoints.TakeDamage(controller.attackDamage);
        }
        else
            currentTime += Time.deltaTime;

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
		Debug.LogWarning("Can't transition to same state");
	}

	#endregion
}
