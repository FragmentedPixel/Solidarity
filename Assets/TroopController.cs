using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopController : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void StartBattle()
    {
        Transform target = FindObjectOfType<TargetHitPoints>().transform;
        agent.SetDestination(target.position);
    }
}
