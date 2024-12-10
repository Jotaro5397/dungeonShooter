using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private float idleTime = 3f; // Adjust this value to determine how long the enemy should idle at each waypoint.

    public override void Enter()
    {
        // Trigger the idle animation or any other behavior you want when entering the IdleState.
        // You can use an "Animator" component or directly modify the enemy's behavior here.
    }

    public override void Perform()
    {
        idleTime -= Time.deltaTime;

        if (idleTime <= 0f)
        {
            stateMachine.ChangeState(new PatrolState()); // Transition back to the PatrolState when the idleTime expires.
        }
    }

    public override void Exit()
    {
        // Clean up or perform any necessary actions when leaving the IdleState.
    }
}
