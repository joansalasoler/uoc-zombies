using System;
using System.Collections;
using UnityEngine;

namespace Game.Shared {

    /**
     * Open a door if the player has the key.
     */
    public class OnDoorTrigger: MonoBehaviour {

        /** If a golden key is required to open the door */
        [SerializeField] private bool goldKey = false;

        /** If a silver key is required to open the door */
        [SerializeField] private bool silverKey = false;

        /** Animator of the door */
        private Animator animator = null;


        /**
         * Initialize the components.
         */
        public void Start() {
            animator = GetComponentInChildren<Animator>();
        }


        /**
         * Fill the player's health reserve if empty.
         */
        private void OnTriggerEnter(Collider collider) {
            if (animator.GetBool("isOpen") == false) {
                if (collider.gameObject.CompareTag("Player")) {
                    PlayerController player = GetPlayerController(collider);
                    bool gold = !goldKey || player.status.goldKey;
                    bool silver = !silverKey || player.status.silverKey;
                    bool isOpen = gold && silver;

                    if (isOpen == true) {
                        animator.SetBool("isOpen", isOpen);
                        AudioService.PlayOneShot(gameObject, "Open Door");
                    }
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
