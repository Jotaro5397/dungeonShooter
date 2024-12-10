using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Animator anim; // call for the animation
    bool inRange = false; // bool to check if player is inside the trigger
    public GameObject GunBody; // hides the gun until you enter the chest
    public GameObject uiAmmo;
    public Key keyScript; // Reference to the Key script

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // starts animation
        GunBody.SetActive(false);
        uiAmmo.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            anim.Play("OpenChest");
            GunBody.SetActive(true);
            uiAmmo.SetActive(true);

            // Call the GetKey() method from the Key script to simulate the key collection
            if (keyScript != null)
            {
                keyScript.GetKey();
            }
        }
    }
}

