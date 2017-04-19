using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrap : MonoBehaviour 
{
	public GameObject obstacle;

	List<TroopHitPoints> troopsCaught = new List<TroopHitPoints>();

	private void OnTriggerEnter(Collider other)
	{
		TroopHitPoints troop = other.transform.GetComponent<TroopHitPoints> ();
		if (troop != null)
			troopsCaught.Add (troop);
	}

	private void OnTriggerExit(Collider other)
	{
		TroopHitPoints troop = other.transform.GetComponent<TroopHitPoints> ();
		if (troop != null)
			troopsCaught.Remove (troop);
	}

	private void Update()
	{
		if (troopsCaught.Count > 3)
			Trap ();
	}

	private void Trap()
	{
		foreach (TroopHitPoints troop in troopsCaught) 
		{
			troop.transform.parent.GetComponent<TroopController> ().Fall ();
		}

		obstacle.SetActive (true);
		Destroy (this);
	}
}
