using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region States
    public iEnemyState currentState;

    public PatrolState patrolState;
    public ChaseState chaseState;
    public AttackState attackState;
    #endregion

    #region Parameters
    [Header("Paramters")]
    public float sightRange;
    public float attackRange;
    public float attackCooldown;
    public float attackDamage;
    public Transform wayPointsParent;
    #endregion

    #region Globals
     public Transform target;
    [HideInInspector] public NavMeshAgent agent;
    #endregion

	private void Start ()
	{
        agent = GetComponent<NavMeshAgent>();

        patrolState = new PatrolState(this);
		chaseState = new ChaseState(this);
		attackState = new AttackState(this);

		currentState = patrolState;
	}

	private void Update ()
	{
		currentState.StateUpdate();
	}

	private void OnTriggerEnter(Collider other)
	{
        currentState.OnTriggerEnter(other);
	}

    private void DebugState()
    {
        if (currentState is ChaseState)
            Debug.Log("C");
        else if (currentState is PatrolState)
            Debug.Log("P");
        else if (currentState is AttackState)
            Debug.Log("A");
    }
}
