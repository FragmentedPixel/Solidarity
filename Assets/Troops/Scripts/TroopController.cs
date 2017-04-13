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
    public float fightRange;
	public float fightCoolDown;
    public float sightRange;
	#endregion

	#region Globals
    public Transform target;
	public List<Transform> targets = new List<Transform>();
    
	public NavMeshAgent agent;
	public List<Vector3> wayPoints = new List<Vector3>();
	#endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        aggroState = new AggroState(this);

        currentState = idleState;
    }

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
		if (enemyHP != null)
			targets.Remove (enemyHP.transform);
	}
	#endregion

	#region Methods

    public void StartBattle()
    {
        // change currentstate to walkstate after the waypoints were set up.
        currentState = walkState;
    }

	public bool SearchForTargets()
	{
		float minDistance = sightRange;
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

		return (target != null);
	}

	public void LookAtTarget()
	{
		Vector3 lookPoint = new Vector3 (target.position.x, transform.position.y, target.position.z);
		transform.LookAt (lookPoint);
	}
	#endregion
}
