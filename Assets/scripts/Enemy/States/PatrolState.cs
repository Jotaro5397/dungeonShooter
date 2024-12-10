using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Collections;
using UnityEngine;

public class PatrolState : BaseState
{
    // Track which waypoint we are currently targeting.
    public int waypointIndex;
    public float waitTimer;
    private bool isWaiting;

    private Animator anim; // Assuming this is the reference to the Animator component.
    private float rotationSpeed = 5.0f;

    public override void Enter()
    {
        anim = enemy.GetComponent<Animator>();
        anim.SetBool("isWalking", true); // Use the 'anim' reference.
        UpdateDestination();
    }

    public override void Perform()
    {
        PatrolCycle();

        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState(enemy)); // Pass the enemy reference
        }

        if (enemy.Agent.remainingDistance > 0.2f)
        {
            // Update isWalking parameter if the enemy is moving
            anim.SetBool("isWalking", true);

            // Adjust the rotation to face the next waypoint
            Vector3 targetDirection = enemy.Agent.steeringTarget - enemy.transform.position;
            if (targetDirection != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(targetDirection);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            // If the enemy is not moving or reached the waypoint, set IsWalking to false
            anim.SetBool("isWalking", false);
        }
    }

    public override void Exit()
    {
        anim.SetBool("isWalking", false); // Use the 'anim' reference.
    }

    public void PatrolCycle()
    {
        // Implement logic for patrol
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                isWaiting = false;
                UpdateDestination();
            }
        }
        else if (enemy.Agent.remainingDistance < 0.2f)
        {
            isWaiting = true;
            waitTimer = 0;
        }
    }

    private void UpdateDestination()
    {
        if (waypointIndex < enemy.path.waypoints.Count - 1)
            waypointIndex++;
        else
            waypointIndex = 0;

        enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
    }
}












