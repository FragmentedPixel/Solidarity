using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
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
    public float attackRange;
    public float attackCooldown;
    public float attackDamage;
    public Transform wayPointsParent;
    #endregion

    #region Globals
	[HideInInspector] public Transform target;
	[HideInInspector] public List<Transform> targets;
	[HideInInspector] public Unit agent;
	[HideInInspector] public Animator anim;
	#endregion

	#region Initialzation
	private void Awake()
	{
        agent = GetComponent<Unit>();
		anim = GetComponentInChildren<Animator> ();

        patrolState = new PatrolState(this);
		chaseState = new ChaseState(this);
		
		currentState = patrolState;
	}
	#endregion

	private void Update ()
	{
		currentState.StateUpdate();
	}

	#region Triggers
	private void OnTriggerEnter(Collider other)
	{
        currentState.OnTriggerEnter(other);

		TroopHitPoints troopHP = other.GetComponent<TroopHitPoints> ();
		if (troopHP != null && !targets.Contains(troopHP.transform))
			targets.Add (troopHP.transform);
	}

	private void OnTriggerExit(Collider other)
	{
		TroopHitPoints troopHP = other.GetComponent<TroopHitPoints> ();

		//if (other.transform == target)
		//	target = null;
	}
	#endregion

	#region Methods

	public void SearchForTargets()
	{
		float newDistance = Mathf.Infinity;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null) // if target died while still in range.
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

		if (target == null)
			currentState.ToPatrolState ();
		else if (newDistance < attackRange)
			currentState.ToAttackState ();
		else
			currentState.ToChaseState ();
	}

	public void LookAtTarget()
	{
		Vector3 lookPoint = new Vector3(target.position.x, transform.position.y, target.position.z);
		transform.LookAt(lookPoint);
	}

	public float DistanceToTarget()
	{
		Vector3 transformPosition = new Vector3 (transform.position.x, 0f, transform.position.z);
		Vector3 targetPosition = new Vector3 (target.position.x, 0f, target.position.z);

		float distance = Vector3.Distance (transformPosition, targetPosition);
		return distance;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * attackRange);
	}
	#endregion
}
