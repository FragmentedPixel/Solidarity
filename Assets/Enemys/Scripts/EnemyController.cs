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
	public List<Transform> targets;
	[HideInInspector] public NavMeshAgent agent;
	#endregion

	private void Awake()
	{
        agent = GetComponent<NavMeshAgent>();

        patrolState = new PatrolState(this);
		chaseState = new ChaseState(this);
		
		currentState = patrolState;
	}

	private void Update ()
	{
		currentState.StateUpdate();
	}

	#region Triggers
	private void OnTriggerEnter(Collider other)
	{
        currentState.OnTriggerEnter(other);

		TroopHitPoints troopHP = other.GetComponent<TroopHitPoints> ();
		if (troopHP != null) 
			targets.Add (troopHP.transform);
	}
	private void OnTriggerExit(Collider other)
	{
        TroopHitPoints troopHP = other.GetComponent<TroopHitPoints> ();

		if (troopHP != null) 
			targets.Remove (troopHP.transform);
	}
	#endregion

	#region Methods

	public bool SearchForTargets()
	{
		float newDistance = sightRange; //if the target is out of sight it won't be count as a target.

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.Remove(targets[i]);
                i--;
            }
            else
            {
                float distance = Vector3.Distance(targets[i].position, transform.position);
                if (distance < newDistance)
                {
                    newDistance = distance;
                    target = targets[i];
                }
            }
        }

		return (target != null);
	}
	public void LookAtTarget()
	{
		Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
		transform.LookAt(lookPoint);
	}
	#endregion
}
