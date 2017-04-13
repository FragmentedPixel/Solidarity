using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : iEnemyState
{
	private int index = -1;

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
        if(other.GetComponent<TroopController>() != null)
        {
            controller.target = other.transform;
			float distance = Vector3.Distance (controller.transform.position, controller.target.position);

			if (distance < controller.attackRange)
				ToAttackState ();
			else if(distance < controller.sightRange)
				ToChaseState ();
        }
    }

    private void Patrol ()
	{
        controller.agent.Resume();
		//TODO: Opreste-l din a merge pana la pozitia fostului target.

		if (controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending) 
		{
			index = (index + 1) % controller.wayPointsParent.childCount; 
			controller.agent.SetDestination(controller.wayPointsParent.GetChild(index).position);
		}
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
