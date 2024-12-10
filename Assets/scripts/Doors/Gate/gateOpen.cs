using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gateOpen : MonoBehaviour
{
    public Animator anim; // call for the animation
    bool inRange = false; // bool to check if player is inside the trigger
    public bool hasKey = false; // bool to check if player has the key 
    public Text keyUsageText;

    public Key keyScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // starts animation
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
   

    // Call this method when the player collects the key
    public void CollectKey()
    {
        hasKey = true;
        // Optionally, you can display a message or play a sound here to indicate that the player collected the key.
    }

    // Call this method when the player uses the key to open the gate
    public void OpenGate()
    {
        if (inRange && hasKey)
        {
            anim.Play("gateUp");
            // Optionally, you can play a sound or particle effect to indicate that the gate is opening.
            // You can also add code to close the gate after a certain amount of time or other conditions.
        }

        if (keyUsageText != null)
        {
            keyUsageText.text = "Key Used!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange && hasKey)
        {
            OpenGate();
            hasKey = false;
            keyScript.UseKey();
        }
        
    }
}

