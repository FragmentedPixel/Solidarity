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
		anim.SetTrigger ("Melee Attack");
		TroopHitPoints troopHitPoints = target.GetComponentInChildren<TroopHitPoints>();
        troopHitPoints.TakeDamage(attackDamage);
    }
}
