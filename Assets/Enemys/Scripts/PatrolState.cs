using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : iEnemyState
{
	private int index = 0;

	public PatrolState(EnemyController enemyController) : base(enemyController)
	{
		
	}

	public override void StateUpdate ()
	{
		Patrol();
	}

    #region Methods

    public override void OnTriggerEnter(Collider other)
    {
		if(other.GetComponent<TroopHitPoints>() != null)
        {
            controller.target = other.transform;

			if (controller.DistanceToTarget() < controller.attackRange)
				ToAttackState ();
			else
				ToChaseState ();
        }
    }

    private void Patrol ()
	{
		CheckCurrentDestionation ();
        controller.agent.Resume();
		controller.anim.SetBool ("Walking", true);

		if (controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending) 
		{
			index = (index + 1) % controller.wayPointsParent.childCount; 
			controller.agent.SetDestination(controller.wayPointsParent.GetChild(index).position);
		}
	}

	private void CheckCurrentDestionation()
	{
		foreach (Transform tran in controller.wayPointsParent)
			if (controller.agent.destination == tran.position)
				return;

		controller.agent.SetDestination (controller.wayPointsParent.GetChild (index).position);
	}
    #endregion

    #region Trnasitions

    public override void ToPatrolState ()
	{
		Debug.LogWarning("Can't transition to same state");
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
