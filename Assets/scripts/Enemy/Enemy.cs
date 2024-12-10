using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }


    //just for debugging.
    [SerializeField]
    public string currentState;
    public Path path;
    private GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    private Transform playerTransform;
    private StateMachine stateMachine;
    public float fireRate = 1.0f;
    public Transform PlayerTransform => playerTransform;
    public Animator anim;
    public Transform target; // Assuming you set the target (player) somewhere

    


    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stateMachine.Initialise();

        // Find the player GameObject and set the reference
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame

    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }
    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // is player close enough to be seen
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer <= fieldOfView / 2f)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hit;
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);

                    if (Physics.Raycast(ray, out hit, sightDistance))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    

}




