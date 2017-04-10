using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class iEnemyState 
{
	public EnemyController controller;

	public iEnemyState (EnemyController enemyController)
	{
		controller = enemyController;
	}

	public abstract void StateUpdate();

	public abstract void ToPatrolState();
	public abstract void ToChaseState();
	public abstract void ToAttackState();
    public abstract void OnTriggerEnter(Collider other);
}
