using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    public Animator anim;
    private StateMachine stateMachine;
    
    [SerializeField]
    private string currentState;
    public Path path;
    private GameObject player;

    public NavMeshAgent Agent { get => agent; }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentState = stateMachine.activeState.ToString();

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 2)
        {
            agent.updatePosition = true;
            agent.SetDestination(target.position);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            agent.updatePosition = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
        }
    }
}
