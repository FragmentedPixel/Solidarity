using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : EnemyController
{
    private void Start()
    {
        attackState = new MeleeAttackState(this);
    }
}
