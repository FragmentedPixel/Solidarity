using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoBehaviour
{
	public float radius;
	public GameObject destinationIndicator;

	private LineRenderer lineRenderer;
	private List<TroopController> troops = new List<TroopController>();
	private bool showPath;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer> ();
	}

	private void Update()
	{
		HideShowPaths ();
		RePathing ();
	}

	#region Compact

	private void HideShowPaths()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			showPath = !showPath;

		if (showPath)
			ShowTroopsPath ();
	}

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
	private void ShowTroopsPath()
	{
		foreach (Transform trans in transform) 
		{
			TroopController controller =  trans.GetComponent<TroopController> ();

			lineRenderer.numPositions = controller.agent.path.corners.Length;

			for (int i = 0; i < lineRenderer.numPositions; i++)
				lineRenderer.SetPosition (i, controller.agent.path.corners [i]);
		}
	}
	public void MoveIndicator()
	{
		Vector3? position = MouseRay ();
		if (position == null)
			destinationIndicator.transform.position = -Vector3.one;
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
		Vector3? position = MouseRay ();
		destinationIndicator.transform.position = -Vector3.one;
		if (position == null)
			return;

		foreach (TroopController troop in troops)
			troop.SetNewDestination (position.Value);
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
