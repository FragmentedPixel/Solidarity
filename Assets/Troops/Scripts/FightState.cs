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
		else if (controller.SearchForTargets ())
			ToAggroState ();
		else
			ToWalkState ();
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

		if (currentTime > controller.fightCoolDown) {
			Fight ();
			currentTime = 0f;
		} else
			currentTime += Time.deltaTime;

		float distance = Vector3.Distance(controller.transform.position, controller.target.position);

		if (distance > controller.sightRange)
			ToWalkState();
		else if (distance > controller.fightRange)
			ToAggroState();
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
