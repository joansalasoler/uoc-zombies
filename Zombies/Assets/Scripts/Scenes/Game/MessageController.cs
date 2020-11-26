using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * Show text messages to the user.
 */
public class MessageController : MonoBehaviour {

    /** A message on the queue */
    private class Message {
        public float delay = 0.0f;
        public string text = string.Empty;
    }

    /** Message queue */
    private Queue<Message> queue = new Queue<Message>();

    /** Text field for the messsages */
    private Text textField = null;


    /**
     * Initialization.
     */
    public void Awake() {
        textField = GetComponentInChildren<Text>(true);
        StartCoroutine(ShowMessages());
    }


    /**
     * Show a text message.
     */
    public void PushMessage(string text, float delay = 0.0f) {
        queue.Enqueue(new Message { text = text, delay = delay });
    }


    /**
     * Coroutine that displays the messages.
     */
    private IEnumerator ShowMessages() {
        while (enabled) {
            if (queue.Count > 0) {
                Message message = queue.Dequeue();
                yield return new WaitForSeconds(message.delay);

                textField.text = message.text;
                textField.gameObject.SetActive(true);
                yield return new WaitForSeconds(4.0f);

                textField.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}
