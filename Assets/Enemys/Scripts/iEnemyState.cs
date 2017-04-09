using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class iEnemyState
{
    private EnemyController controller;

    public iEnemyState(EnemyController enemyController)
    {
        controller = enemyController;
    }
}
