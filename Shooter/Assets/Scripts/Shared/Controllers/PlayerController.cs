using System;
using UnityEngine;
using System.Collections;

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
        public static Action<PlayerController> playerDamaged;

        /** Invoked when the player is killed */
        public static Action<PlayerController> playerKilled;

        /** Current player status */
        public PlayerStatus status = null;


        /**
         * Ensure the status is fresh.
         */
        private void Start() {
            status.Refresh();
        }


        /**
         * Handles the player input.
         */
        private void Update() {
            Vector3 velocity = characterController.velocity;
            float speed = velocity.magnitude;

            weaponsController.SetSpeed(speed);

            if (Input.GetButtonUp("Fire2")) {
                weaponsController.ToggleWeapon();
            }

            if (status.HasMunition() && Input.GetButton("Fire1")) {
                if (weaponsController.ShootWeapon()) {
                    status.DecreaseWater();
                }
            }
        }


        /**
         * Cause damage to this player.
         */
        public override void Damage() {
            if (status.DamagePlayer()) {
                playerDamaged.Invoke(this);
            } else {
                Kill();
            }
        }


        /**
         * Kills this player.
         */
        public override void Kill() {
            base.Kill();
            playerKilled.Invoke(this);
        }
    }
}
