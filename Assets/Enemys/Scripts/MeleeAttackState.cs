using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    public MeleeAttackState(EnemyController enemyController) : base(enemyController)
    {

    }

    float currentTime = 0f;

    public override void StateUpdate()
    {
        if (!controller.target)
            CheckForOtherTargets();
        else
            FightTarget();
    }

    #region Methods

    private void Look()
    {
        Vector3 lookPoint = new Vector3(controller.target.position.x, controller.transform.position.y, controller.target.position.z);
        controller.transform.LookAt(lookPoint);
    }

    public void Attack()
    {
        controller.agent.Stop();

        if (currentTime > controller.attackCooldown)
        {
            Debug.Log("Player should melee attack");
            currentTime = 0f;
            TroopHitPoints troopHitPoints = controller.target.GetComponent<TroopHitPoints>();
            troopHitPoints.TakeDamage(controller.attackDamage);
        }
        else
            currentTime += Time.deltaTime;

    }

    private void FightTarget()
    {
        Look();
        Attack();

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);

        if (distance > controller.sightRange)
            ToPatrolState();
        else if (distance > controller.attackRange)
            ToChaseState();
    }

    private void CheckForOtherTargets()
    {
        TroopHitPoints[] troops = GameObject.FindObjectsOfType<TroopHitPoints>();

        float closestDistance = Mathf.Infinity;

        foreach (TroopHitPoints troopHP in troops)
        {
            float distance = Vector3.Distance(controller.transform.position, troopHP.transform.position);
            if (distance < controller.sightRange && distance < closestDistance)
            {
                closestDistance = distance;
                controller.target = troopHP.transform;
            }
        }

        if (!controller.target)
            ToPatrolState();
        else
            ToChaseState();


    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    #endregion

    #region Trnasitions

    public override void ToPatrolState()
    {
        controller.currentState = controller.patrolState;
    }

    public override void ToChaseState()
    {
        controller.currentState = controller.chaseState;
    }

    public override void ToAttackState()
    {
        Debug.LogWarning("Can't transition to same state");
    }

    #endregion
}
