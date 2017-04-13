using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopRangedController : TroopController 
{
	private void Start () 
	{
		fightState = new RangedFightState (this);	
	}

	public void ShootProjectile()
	{
		Debug.Log ("Player should shoot.");
	}
}
