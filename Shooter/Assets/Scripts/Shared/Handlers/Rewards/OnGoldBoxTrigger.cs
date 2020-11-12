using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles collisions with gold key box rewards.
     */
    public class OnGoldBoxTrigger: MonoBehaviour {

        /**
         * Fill the player's health reserve if empty.
         */
        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                AudioService.PlayOneShot(collider.gameObject, "Collect Key");
                PlayerController player = GetPlayerController(collider);
                player.status.CollectGoldKey();
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
