using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : iTroopState
{
    public FightState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {
        Fight();

        float distance = Vector3.Distance(controller.transform.position, controller.target.position);
        if (distance > controller.sightRange)
            ToWalkState();
        if (distance > controller.attackRange)
            ToAggroState();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {

    }

    private void Fight()
    {
        controller.agent.Stop();
    }
    
    #endregion

    #region Transitions
    public override void ToFightState()
    {
        Debug.LogWarning("Can't transition to same state");
    }

    public override void ToAggroState()
    {
        controller.currentState = controller.aggroState;
    }

    public override void ToWalkState()
    {
        controller.currentState = controller.walkState;
    }
    #endregion

}
