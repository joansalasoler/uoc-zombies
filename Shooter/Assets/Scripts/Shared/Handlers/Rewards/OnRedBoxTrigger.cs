using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles collisions with red key box rewards.
     */
    public class OnRedBoxTrigger: MonoBehaviour {

        /**
         * Fill the player's health reserve if empty.
         */
        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                AudioService.PlayOneShot(collider.gameObject, "Collect Key");
                PlayerController player = GetPlayerController(collider);
                player.status.CollectRedKey();
                Destroy(gameObject, 0.5f);
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
