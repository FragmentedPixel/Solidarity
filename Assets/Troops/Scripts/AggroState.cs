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
		if (controller.target != null)
			Aggro ();
		else
			controller.SearchForTargets ();
    }

    #region Methods
    public override void OnTriggerEnter(Collider other)
    {
		
    }

    private void Aggro()
    {
		controller.agent.SetDestination(controller.target);
		controller.LookAtTarget ();

		if (controller.DistanceToTarget() < controller.fightRange)
			ToFightState();
    }
    #endregion

    #region Transitions
	public override void ToIdleState()
	{
		controller.currentState = controller.idleState;
	}

    public override void ToFightState()
    {
        controller.currentState = controller.fightState;
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
