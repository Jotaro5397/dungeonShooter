using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private Enemy enemy;
    private float shotTimer;
    public float fireRate = 1.0f;

    public AttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        
    }
    
    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.target.transform);
            //if shot timer > fireRate
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //change state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {
        // Implementation for Exit method in AttackState
    }

    public void Shoot()
    {
        Debug.Log("shoot");
        shotTimer = 0;
    }


    void Start()
    {
        
    }
}



