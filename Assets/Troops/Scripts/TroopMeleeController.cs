using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMeleeController : TroopController 
{
	private void Start () 
	{
		fightState = new MeleeFightState (this);	
	}

	public void DealDamage()
	{
		EnemyHitPoints enemyHP = target.GetComponent<EnemyHitPoints> ();
		enemyHP.TakeDamage (attackDamage);
	}
}
