using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles collisions with silver key box rewards.
     */
    public class OnSilverBoxTrigger: MonoBehaviour {

        /**
         * Fill the player's health reserve if empty.
         */
        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                PlayerController player = GetPlayerController(collider);
                player.status.CollectSilverKey();
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
