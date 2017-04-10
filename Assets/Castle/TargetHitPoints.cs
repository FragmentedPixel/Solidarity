using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitPoints : MonoBehaviour
{
    public float targetHitPoints;

    public void TakeDamage(float damageToTake)
    {
        targetHitPoints -= damageToTake;

        if (targetHitPoints <= 0f)
            Destroy(gameObject);
    }

}
