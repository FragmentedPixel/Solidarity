using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFightState : FightState 
{
	public TroopMeleeController meleeController;

	public MeleeFightState(TroopMeleeController troopMeleeController) : base(troopMeleeController as TroopController)
	{
		meleeController = troopMeleeController;
	}

	public override void Fight()
	{
		Debug.Log("Play Enemy melee attack animation");
		meleeController.DealDamage();
	}
}
