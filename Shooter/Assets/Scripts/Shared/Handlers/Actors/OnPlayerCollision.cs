using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Behaviour for player collisions with movable and moving bodies.
     */
    public class OnPlayerCollision: MonoBehaviour {

        /** Force to move objects */
        public float pushForce = 2.0F;

        /** Character controller instance */
        private CharacterController controller = null;

        /** Velocity of the platform the player is on top */
        private Vector3 inertia = Vector3.zero;


        /**
         * Initialize this handler.
         */
        public void Start() {
            controller = GetComponent<CharacterController>();
        }


        /**
         * Updates the player position according to the velocity of the
         * platform where the player is grounded. If the player is not
         * grounded, incrementaly reduce the inertial velocity.
         */
        private void FixedUpdate() {
            if (inertia.magnitude > 0.01f) {
                controller.transform.position += inertia * Time.fixedDeltaTime;
                Physics.SyncTransforms();
            }

            if (controller.isGrounded == false) {
                inertia -= 0.05f * inertia.normalized;
            }
        }


        /**
         * Handles player collisions with movable objects and platforms.
         */
        public void OnControllerColliderHit(ControllerColliderHit hit) {
            if (IsPlayerOnTop()) {
                inertia = Vector3.zero;

                if (hit.collider.CompareTag("Platform")) {
                    UpdatePlayerInertia(hit);
                }
            } else {
                if (hit.collider.CompareTag("Monster")) {
                    PushColliderBody(hit);
                }

                if (hit.collider.CompareTag("Moveable")) {
                    PushColliderBody(hit);
                }
            }
        }


        /**
         * Push an object if a collision happened on the sides.
         */
        private void PushColliderBody(ControllerColliderHit hit) {
            Vector3 force = pushForce * controller.velocity;
            Rigidbody body = hit.collider.attachedRigidbody;
            body.AddForceAtPosition(force, hit.point);
        }


        /**
         * Set a new inertial velocity for the player when on top of
         * a moving platform.
         */
        private void UpdatePlayerInertia(ControllerColliderHit hit) {
            Rigidbody body = hit.collider.attachedRigidbody;
            inertia = body.velocity;
        }


        /**
         * Checks if the player collided on the top.
         */
        private bool IsPlayerOnTop() {
            return controller.collisionFlags == CollisionFlags.Below;
        }
    }
}
