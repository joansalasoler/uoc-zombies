using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Game.Shared {

    /**
     * Controller for player characters.
     */
    public class PlayerController : ActorController {

        /** Character controller instance */
        [SerializeField] private CharacterController characterController = null;

        /** Weapon controller instance */
        [SerializeField] private WeaponController weaponsController = null;

        /** Invoked when the player is damaged */
        public Action<PlayerController> playerDamaged;

        /** Invoked when the player is killed */
        public Action<PlayerController> playerKilled;

        /** Current player status */
        public PlayerStatus status = null;

        /** Current moving speed */
        public float speed = 0.0f;


        /**
         * Disable the character controller.
         */
        public void DisableController() {
            GetComponent<FirstPersonController>().enabled = false;
            characterController.enabled = false;
        }


        /**
         * Handles the player input.
         */
        private void Update() {
            if (isAlive == false) {
                return;
            }

            speed = characterController.velocity.magnitude;
            weaponsController.SetSpeed(speed);

            if (Input.GetButtonUp("Fire2")) {
                weaponsController.ToggleWeapon();
            }

            if (Input.GetButton("Fire1") && weaponsController.CanShootWeapon()) {
                if (status.HasMunition() == false) {
                    weaponsController.ShootNothing();
                } else {
                    weaponsController.ShootWeapon();
                    status.DecreaseWater();
                }
            }
        }


        /**
         * Cause damage to this player.
         */
        public override void Damage() {
            if (isAlive == false) {
                return;
            }

            if (status.DamagePlayer()) {
                AudioService.PlayOneShot(gameObject, "Damage Player");

                if (playerDamaged != null) {
                    playerDamaged.Invoke(this);
                }
            } else {
                Kill();
            }
        }


        /**
         * Kills this player.
         */
        public override void Kill() {
            if (isAlive == false) {
                return;
            }

            AudioService.PlayOneShot(gameObject, "Player Die");
            weaponsController.SetAliveState(false);
            base.Kill();

            if (playerKilled != null) {
                playerKilled.Invoke(this);
            }
        }
    }
}
