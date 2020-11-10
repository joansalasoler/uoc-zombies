using System;
using System.Collections;
using UnityEngine;

namespace Game.Shared {

    /**
     * Activate the castle's elevator on collision.
     */
    public class OnElevatorTrigger: MonoBehaviour {

        /** Top position of the elevator */
        public float top = 30.0f;

        /** Bottom position of the elevator */
        public float bottom = 24.4f;

        /** Speed of the elevator */
        public float speed = 2.0f;

        /** If the elevator is currently moving */
        private bool isStopped = true;

        /** If the elevator was moved down */
        private bool isDown = false;

        /** Direction to move the elevator */
        private Vector3 direction = Vector3.zero;

        /** Rigidbody of the elevator */
        private Rigidbody body = null;


        /**
         * Move the elevator in the up direction.
         */
        public void MoveUp() {
            direction = Vector3.up;
            isStopped = false;
        }


        /**
         * Move the elevator in the down direction.
         */
        public void MoveDown() {
            direction = Vector3.down;
            isStopped = false;
        }


        /**
         * Initialize the components.
         */
        private void Start() {
            body = GetComponent<Rigidbody>();
        }


        /**
         * Fill the player's health reserve if empty.
         */
        private void OnTriggerEnter(Collider collider) {
            if (isStopped && collider.gameObject.CompareTag("Player")) {
                if (isDown) MoveUp(); else MoveDown();
            }
        }


        /**
         * Moves the elevator on the given direction and stops when
         * the top or bottom positions are reached.
         */
        private void FixedUpdate() {
            if (isStopped == true) {
                direction = Vector3.zero;
            }

            float y = transform.position.y;

            if (isDown && y >= top) {
                direction = Vector3.zero;
                isStopped = true;
                isDown = false;
            } else if (!isDown && y <= bottom) {
                direction = Vector3.zero;
                isStopped = true;
                isDown = true;
            }

            body.velocity = speed * direction;
        }
    }
}
