using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    public EnemyMeleeController meleeController;

    public MeleeAttackState(EnemyMeleeController enemyMeleeController) : base(enemyMeleeController as EnemyController)
    {
        meleeController = enemyMeleeController;
    }

    public override void Attack()
    {
        meleeController.DealDamage();
    }
}
