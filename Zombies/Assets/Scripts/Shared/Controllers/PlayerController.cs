using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Game.Shared {

    /**
     * Controller for player characters.
     */
    public class PlayerController : ActorController {

        /** Weapon controller instance */
        [SerializeField] private WeaponController weaponController = null;

        /** Animator for the player's character */
        private Animator animator = null;

        /** Invoked when the player is damaged */
        public Action<PlayerController> playerDamaged;

        /** Invoked when the player is killed */
        public Action<PlayerController> playerKilled;

        /** Current player status */
        public PlayerStatus status = null;


        /**
         * Initialization.
         */
        private void Start() {
            animator = GetComponent<Animator>();
        }


        /**
         * Handles the player input.
         */
        private void Update() {
            if (isAlive == false) {
                return;
            }

            if (Input.GetButtonUp("Fire1")) {
                animator.SetTrigger("Fire");
            }
        }


        /**
         * Invoked on the weapon animation on the shot frame.
         */
        private void OnWeaponShot() {
            if (weaponController.CanShootWeapon()) {
                if (status.HasMunition() == false) {
                    weaponController.ShootNothing();
                } else {
                    weaponController.ShootWeapon();
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
            base.Kill();

            if (playerKilled != null) {
                playerKilled.Invoke(this);
            }
        }
    }
}
