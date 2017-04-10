using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopHitPoints : MonoBehaviour
{
    public float hitPoints;

    public void TakeDamage(float damageToTake)
    {
        hitPoints -= damageToTake;

        if (hitPoints <= 0f)
            Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Check if there are any collisions with enemy projectiles.
    }

}
