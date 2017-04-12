using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
	public EnemyRangedController rangedController;

	public RangedAttackState(EnemyRangedController enemyRangedController) : base(enemyRangedController as EnemyController)
    {
		rangedController = enemyRangedController;
    }

    public override void Attack()
    {
        Debug.Log("Play enemy ranged attack animation");
        rangedController.ShootProjectile();
    }
}
