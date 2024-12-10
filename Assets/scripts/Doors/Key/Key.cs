using UnityEngine;
using UnityEngine.UI;
using UnityEngine;


public class Key : MonoBehaviour
{
    public string collectedMessage = "You've collected the key!";
    public string usedMessage = "You've used the key!";
    public KeyUI keyUI; // Reference to the KeyUI GameObject
    public gateOpen gateScript; // Reference to the script that opens the gate

    private bool isKeyUsed = false;


    // Call this method when the player automatically gets the key (e.g., on level start)
    public void GetKey()
    {
        // you can call the CollectKey() method directly to simulate the key collection.
        CollectKey();
    }

    // Called when the player collects the key
    private void CollectKey()
    {
        if (gateScript != null)
        {
            gateScript.hasKey = true; // Simulate the player having the key
            gateScript.OpenGate();    // Automatically open the gate when the player gets the key

            // Show the collected message using the KeyUI
            if (keyUI != null)
            {
                keyUI.ShowMessage(collectedMessage);
            }

            // Optionally, you may want to play a sound or a particle effect when the key is collected.
            // You can add those effects here.
            // ...

            // The key doesn't need to be destroyed here since it was never physically present in the game world.
        }
    }

    public void UseKey()
    {
        // Only show the key used message if the key hasn't been used before
        if (!isKeyUsed)
        {


            if (keyUI != null)
            {
                keyUI.ShowMessage(usedMessage);
            }

            isKeyUsed = true; // Set the flag to true, so the message won't play again.
        }
    }
}




