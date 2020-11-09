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

        /** Current shield points */
        public int shieldPoints = 6;

        /** Current health points */
        public int healthPoints = 18;

        /** Water drops that cna be shot */
        public int waterDrops = 20;

        /** Inventory of this player */
        public PlayerStock stock = null;


        /**
         * Cause damage to this player.
         */
        public override void Damage() {
            shieldPoints -= 1;
            healthPoints -= shieldPoints >= 0 ? 1 : 2;
            if (healthPoints <= 0) Kill();
            playerDamaged.Invoke(this);
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
