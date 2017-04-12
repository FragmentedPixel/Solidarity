using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : EnemyController
{
    private void Start()
    {
        attackState = new MeleeAttackState(this);
    }

    public void DealDamage()
    {
        TroopHitPoints troopHitPoints = target.GetComponent<TroopHitPoints>();
        troopHitPoints.TakeDamage(attackDamage);
    }
}
