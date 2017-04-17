using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFightState : FightState 
{
	public TroopRangedController rangedController;

	public RangedFightState(TroopRangedController troopRangedController) : base(troopRangedController as TroopController)
	{
		rangedController = troopRangedController;
	}

	public override void Fight()
	{
		Debug.Log("Play Enemy ranged attack animation");
		rangedController.ShootProjectile();
	}
}
