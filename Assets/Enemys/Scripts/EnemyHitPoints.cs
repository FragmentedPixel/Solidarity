using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPoints : MonoBehaviour
{
	public Transform hitTarget;
    public float health;
    public float priority;

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

		if (health <= 0f)
			Destroy (transform.parent.gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
		TroopProjectile proj = other.transform.GetComponent<TroopProjectile>();

		if(proj != null)
		{
			TakeDamage(proj.damage);
			Destroy(proj.gameObject);
		}
	}
}
