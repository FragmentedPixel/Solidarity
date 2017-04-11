using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : MonoBehaviour
{
    public iTroopState currentState;

    public IdleState idleState;
    public WalkState walkState;
    public AggroState aggroState;
    public FightState fightState;

    public float attackRange;
    public float sightRange;

    public Transform target;
    public NavMeshAgent agent;
    public Vector3[] wayPoints;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        aggroState = new AggroState(this);
        fightState = new FightState(this);

        currentState = idleState;
    }

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    public void StartBattle()
    {
        // change currentstate to walkstate after the waypoints were set up.
        currentState = walkState;
    }
}
