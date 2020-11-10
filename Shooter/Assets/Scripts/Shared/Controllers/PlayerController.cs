using System;
using UnityEngine;
using System.Collections;

namespace Game.Shared {

    /**
     * Controller for player characters.
     */
    public class PlayerController : ActorController {

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
