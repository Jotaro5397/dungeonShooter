using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public BaseState activeState;
    
    //property for the patrol state.


    public void Initialise()
    {
        //setup default state.
        ChangeState( new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   public void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        // check activeStaet != null// check activeStaet != null
        if (activeState != null)
        {
            //run cleanup on activeState
            activeState.Exit();
        }
        //change to a new state.
        activeState = newState;

        //fail-safe null check to make sure new state wasn't null
        if (activeState != null)
        {
            //Setup new state
            activeState.stateMachine = this;
            //assign state enemy class.
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }

    }
}
