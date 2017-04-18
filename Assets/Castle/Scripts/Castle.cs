using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour 
{
	public Canvas winCanvas;

	private bool won;

	private void Start()
	{
		won = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		TroopHitPoints troopHP = other.transform.GetComponent<TroopHitPoints> ();
		if (troopHP != null)
			Win ();
	}

	private void Win()
	{
		if (won)
			return;

		won = true;
		winCanvas.enabled = true;
		Time.timeScale = 0f;
	}
}
