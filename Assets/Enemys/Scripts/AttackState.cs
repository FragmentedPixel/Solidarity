using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : iEnemyState
{
    public AttackState(EnemyController enemyController) : base(enemyController)
    {

    }
}
