using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
	[HideInInspector] public float damage;

    private Transform target;

    public void SetTarget(float projDamage, Transform projTarget)
    {
        damage = projDamage;
        target = projTarget;
    }

    private void FixedUpdate()
    {
        if (target)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        else
            Destroy(gameObject);
    }
}
