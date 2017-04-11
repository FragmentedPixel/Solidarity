using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : iTroopState
{
    public IdleState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {

    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
        
    }
    #endregion

    #region Transitions
    public override void ToFightState()
    {
        
    }

    public override void ToAggroState()
    {

    }

    public override void ToWalkState()
    {

    }
    #endregion

}
