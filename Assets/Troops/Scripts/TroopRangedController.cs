using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopRangedController : TroopController 
{
	[Header("Projectile")]
	public GameObject projectile;
	public Transform muzzle;

	private void Start() 
	{
		fightState = new RangedFightState (this);	
	}

	public void ShootProjectile()
	{
		GameObject proj = Instantiate(projectile, muzzle.position, muzzle.rotation);
		EnemyHitPoints enemyHP = target.GetComponentInChildren<EnemyHitPoints> ();
		proj.GetComponent<TroopProjectile>().SetTarget(attackDamage, enemyHP.hitTarget);
	}
}
