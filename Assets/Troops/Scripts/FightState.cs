using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightState : iTroopState
{
	public float currentTime;

    public FightState(TroopController troopController) : base(troopController)
    {

    }

    public override void StateUpdate()
    {
		if (controller.target)
			FightTarget ();
		else
			controller.SearchForTargets ();
    }

    #region Methods
	public abstract void Fight ();

    public override void OnTriggerEnter(Collider other)
    {

    }

	private void FightTarget()
    {
        controller.agent.Stop();
		controller.LookAtTarget ();

		if (currentTime > controller.fightCoolDown) 
		{
			Fight ();
			currentTime = 0f;
		} 
		else
			currentTime += Time.deltaTime;

		if (controller.DistanceToTarget() > controller.fightRange)
			ToAggroState();
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
