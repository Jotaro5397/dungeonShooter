using UnityEngine;
using UnityEngine.UI;


public class KeyUI : MonoBehaviour
{
    public float displayDuration = 2.0f; // How long to display the message in seconds
    private Text messageText;
    private bool displayingMessage = false;

    void Start()
    {
        messageText = GetComponent<Text>();
        messageText.enabled = false;
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.enabled = true;
        displayingMessage = true;
        Invoke("HideMessage", displayDuration);
        Debug.Log("Showing message: " + message);
        // The rest of your code to display the message...
    }

    private void HideMessage()
    {
        messageText.enabled = false;
        displayingMessage = false;
    }

    public bool IsDisplayingMessage()
    {
        return displayingMessage;
    }

}
