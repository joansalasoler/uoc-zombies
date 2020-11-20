using System;
using System.Collections;
using UnityEngine;

namespace Game.Shared {

    /**
     * Open the spaceship if the player has the key.
     */
    public class OnSpaceshipTrigger: MonoBehaviour {

        /** Message overlay controller */
        public MessageController message;

        /** If the player has visited the spaceship before */
        private bool isMemoryRecovered = false;

        /** If the spaceship was opened with a red key */
        private bool isMissionComplete = false;


        /**
         * Show a message to the player.
         */
        private void OnTriggerEnter(Collider collider) {
            if (isMissionComplete) {
                return;
            }

            if (collider.gameObject.CompareTag("Player")) {
                PlayerController player = GetPlayerController(collider);

                if (player.status.redKey) {
                    message.ShowMessage("WELL DONE! I CAN RETURN HOME NOW");
                    isMissionComplete = true;
                } else if (isMemoryRecovered == false){
                    message.ShowMessage("OH, I REMEMBER NOW! I LOST THE KEYS OF MY SPACESHIP!");
                    isMemoryRecovered = true;
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
