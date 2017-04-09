using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoBehaviour
{
    public void StartBattle()
    {
        foreach(Transform t in transform)
        {
            t.GetComponent<TroopController>().StartBattle();
        }
    }

}
