using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles collisions with ocean.
     */
    public class OnOceanTrigger: MonoBehaviour {

        /** Last time the tank was refilled */
        private float lastRefill = 0.0f;


        /**
         * Fill the player's water reserve tank each second the player
         * is on the ocean's waters.
         */
        private void OnTriggerStay(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                if (Time.time - lastRefill >= 1.0f) {
                    PlayerController player = GetPlayerController(collider);
                    player.status.InreaseWater();
                    lastRefill = Time.time;
                }
            }
        }


        /**
         * Obtain the player's controller from the collider.
         */
        private PlayerController GetPlayerController(Collider collider) {
            return collider.GetComponent<PlayerController>();
        }
    }
}
