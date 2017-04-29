using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrap : MonoBehaviour 
{
	public GameObject obstacle;
	public Rigidbody podRb;

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
		if (troopsCaught.Count >= 2)
			Trap ();
	}

	private void Trap()
	{
		foreach (TroopHitPoints troop in troopsCaught) 
		{
			troop.transform.parent.GetComponent<TroopController> ().Fall ();
		}
		podRb.useGravity = true;
		obstacle.SetActive (true);
		Destroy (this);
	}
}
