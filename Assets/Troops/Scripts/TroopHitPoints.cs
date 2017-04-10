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

    private void OnCollisionEnter(Collision other)
    {
        EnemyProjectile proj = other.transform.GetComponent<EnemyProjectile>();

        if(proj != null)
        {
            TakeDamage(proj.damage);
            Destroy(proj.gameObject);
        }
    }

}
