using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroState : iTroopState
{
    public AggroState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {
		if (controller.target)
			Aggro ();
		else if (!controller.SearchForTargets ())
			ToWalkState ();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
		
    }

    private void Aggro()
    {
		controller.agent.Resume ();
        controller.agent.SetDestination(controller.target.position);

		float distance = Vector3.Distance(controller.transform.position, controller.target.position);
		if (distance > controller.sightRange)
			ToWalkState();
		if (distance < controller.fightRange)
			ToFightState();
    }
    #endregion

    #region Transitions
    public override void ToFightState()
    {
        controller.currentState = controller.fightState;
    }

    public override void ToAggroState()
    {
        Debug.LogWarning("Can't transition to same state");
    }

    public override void ToWalkState()
    {
        controller.currentState = controller.walkState;
    }
    #endregion

}
