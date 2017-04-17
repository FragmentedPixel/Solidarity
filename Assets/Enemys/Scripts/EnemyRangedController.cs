using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : EnemyController
{
    [Header("Projectile")]
    public GameObject projectile;
    public Transform muzzle;

    private void Start()
    {
        attackState = new RangedAttackState(this);
    }

    public void ShootProjectile()
    {
        GameObject proj = Instantiate(projectile, muzzle.position, muzzle.rotation);
		TroopHitPoints troopHP = target.GetComponentInChildren<TroopHitPoints> ();
		proj.GetComponent<EnemyProjectile>().SetTarget(attackDamage, troopHP.hitTarget);
    }
}
