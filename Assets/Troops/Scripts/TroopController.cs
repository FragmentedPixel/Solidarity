using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : MonoBehaviour
{
	#region States
    public iTroopState currentState;

    public IdleState idleState;
    public WalkState walkState;
    public AggroState aggroState;
    public FightState fightState;
	#endregion

	#region Parameters
	[Header("Parameters")]
    public float fightRange;
	public float fightCoolDown;
	public float attackDamage;
	#endregion

	#region Globals
	[Header("Testing")]
	private bool battleStarted = false;
	[HideInInspector] public Transform target;
	[HideInInspector] public List<Transform> targets = new List<Transform>();
    
	[HideInInspector] public aAgent agent;
    [HideInInspector] public Transform destination;
	#endregion

	#region Initialization
    private void Awake()
    {
        agent = GetComponent<aAgent>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        aggroState = new AggroState(this);

        currentState = idleState;

		destination = FindObjectOfType<Castle> ().transform;
	}
	#endregion

    private void Update()
    {
        currentState.StateUpdate();
    }

	#region Triggers

    private void OnTriggerEnter(Collider other)
    {
		EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints> ();
		if (enemyHP != null && !targets.Contains(enemyHP.transform))
			targets.Add (enemyHP.transform);

        currentState.OnTriggerEnter(other);
    }

	private void OnTriggerExit(Collider other)
	{
		EnemyHitPoints enemyHP = other.transform.GetComponent<EnemyHitPoints> ();

		//if (other.transform == target)
		//	target = null;
	}
	#endregion

	#region Methods
    public void StartBattle()
    {
		battleStarted = true;
        currentState = walkState;
    }
	public void SearchForTargets()
	{
		float minDistance = Mathf.Infinity;
		for (int i = 0; i < targets.Count; i++) 
		{
			if (targets [i] == null) 
			{
				targets.Remove (targets [i]);
				i--;
			} 
			else 
			{
				float newDistance = Vector3.Distance (transform.position, targets [i].position);
				if (newDistance < minDistance) 
				{
					minDistance = newDistance;
					target = targets [i];
				}
			}
		}

		if (target == null)
			currentState.ToWalkState ();
		else if (minDistance < fightRange)
			currentState.ToFightState ();
		else
			currentState.ToAggroState ();
	}
	public void LookAtTarget()
	{
		Vector3 lookPoint = new Vector3 (target.position.x, transform.position.y, target.position.z);
		transform.LookAt (lookPoint);
	}
	public void SetNewDestination(Transform newDestination)
	{
		destination = newDestination;

		if(battleStarted)
			currentState.ToWalkState ();
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
		Gizmos.DrawLine (transform.position, transform.position + transform.forward * fightRange);
	}
	public void Fall()
	{
		agent.enabled = false;
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.useGravity = true;
		Destroy (this);
	}
	#endregion
}
