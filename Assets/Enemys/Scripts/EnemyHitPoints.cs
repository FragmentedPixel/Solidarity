using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPoints : MonoBehaviour
{
    public float health;
    public float priority;

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0f)
            Destroy(gameObject);
    }
}
