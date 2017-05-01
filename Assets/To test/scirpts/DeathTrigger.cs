using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour 
{
	private void OnTriggerEnter(Collider other)
	{
		TroopHitPoints troopHP = other.transform.GetComponent<TroopHitPoints> ();
		if (troopHP != null)
			Destroy (troopHP.transform.parent.gameObject);
	}

}
