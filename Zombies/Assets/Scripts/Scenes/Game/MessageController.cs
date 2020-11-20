using System;
using UnityEngine;
using UnityEngine.UI;


/**
 * Show text messages to the user.
 */
public class MessageController : MonoBehaviour {

    /**
     * Show a text message.
     */
    public void ShowMessage(string text) {
        GetComponentInChildren<Text>().text = text;
        gameObject.SetActive(true);
        Invoke("HideMessage", 4.0f);
    }


    /**
     * Hide this overlay.
     */
    public void HideMessage() {
        gameObject.SetActive(false);
    }
}
