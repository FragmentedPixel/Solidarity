using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoBehaviour
{
    private void Update()
    {

    }

    public void StartBattle()
	{
		foreach(Transform t in transform)
		{
			t.GetComponent<TroopController>().StartBattle();
		}
	}
}
