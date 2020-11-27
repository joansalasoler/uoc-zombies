using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Shared;

namespace Game.Shared {

    /**
     * Show a message to the player when a collision occurs whith the field
     * of view of the character. Each message will only be shown once,
     * regardless of the object where it is attached.
     */
    public class OnPlayerMessageTrigger : MonoBehaviour {

        /** Set of all the messages */
        private static HashSet<string> messages = new HashSet<string>();

        /** Text of the message */
        [SerializeField] private string text = string.Empty;


        /**
         * Initialization.
         */
        private void Awake() {
            messages.Add(text);
        }


        /**
         * Attach the event handlers.
         */
        private void OnEnable() {
            OnFieldOfViewTrigger.onTriggerEnter += OnFieldOfViewEnter;
        }


        /**
         * Dettach the event handlers.
         */
        private void OnDisable() {
            OnFieldOfViewTrigger.onTriggerEnter -= OnFieldOfViewEnter;
        }


        /**
         * Show a message when the collision happens.
         */
        private void OnFieldOfViewEnter(Collider collider) {
            if (enabled && gameObject == collider.gameObject) {
                if (messages.Contains(text)) {
                    MessageService.Push(text);
                    messages.Remove(text);
                }

                Destroy(this);
            }
        }
    }
}
