﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class iTroopState
{
    public TroopController controller;

    public iTroopState(TroopController troopController)
    {
        controller = troopController;
    }

    public abstract void StateUpdate();
    public abstract void OnTriggerEnter(Collider other);
    
	public abstract void ToIdleState();
    public abstract void ToWalkState();
    public abstract void ToAggroState();
    public abstract void ToFightState();
}
