using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoBehaviour
{
	public GameObject destinationIndicator;

	#region Compact

	private void RePathing()
	{
		if (Input.GetMouseButtonDown (0))
			SelectTroops ();
		else if (Input.GetMouseButtonUp (0))
			SendTroops ();
		else if (Input.GetMouseButton (0))
			MoveIndicator ();
	}

	#endregion

	#region Methods
	public void StartBattle()
	{
		foreach(Transform t in transform)
		{
			t.GetComponent<TroopController>().StartBattle();
		}
	}

    public void MoveIndicator()
	{
		Vector3? position = MouseRay ();
		if (position == null)
			destinationIndicator.transform.position = initDestinationPosition;
		else
			destinationIndicator.transform.position = position.Value;
	}
	private void SelectTroops()
	{
		Vector3? position = MouseRay ();
		if (position == null)
			return;
	
		TroopController[] allTroops = FindObjectsOfType<TroopController> ();
		troops.Clear ();

		foreach (TroopController troop in allTroops) 
		{
			float dist = Vector3.Distance (position.Value, troop.transform.position);

			if (dist < radius)
				troops.Add (troop);
		}
	}
	public void SendTroops()
	{
		
	}
	#endregion

	#region Utility
	private Vector3? MouseRay()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayInfo;

		if (Physics.Raycast (ray, out rayInfo) && rayInfo.transform.CompareTag ("Terrain"))
			return rayInfo.point;
		else
			return null;
	}
	#endregion
}
