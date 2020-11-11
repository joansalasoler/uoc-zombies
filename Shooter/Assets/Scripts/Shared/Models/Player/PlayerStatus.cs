using System;
using System.ComponentModel;
using UnityEngine;

namespace Game.Shared {

    /**
     * Encapsulates the current status of the player.
     */
    [CreateAssetMenu]
    public class PlayerStatus : ScriptableObject {

        /** Invoked when a property on this status changes */
        public Action<PlayerStatus> statusChanged;

        /** If the player collected a golden key */
        public bool goldKey = false;

        /** If the player collected a silver key */
        public bool silverKey = false;

        /** Current shield points */
        public int shieldPoints = 3;

        /** Current health points */
        public int healthPoints = 10;

        /** Water drops that can be shot */
        public int waterDrops = 5;

        /** Maximum number of shield points */
        public int shieldSlots = 3;

        /** Maximum number of health points */
        public int healthSlots = 10;

        /** Water drops that can be collected */
        public int waterSlots = 10;


        /**
         * Refresh this player status.
         */
        public void Refresh() {
            statusChanged.Invoke(this);
        }


        /**
         * Collect a golden key.
         */
        public void CollectGoldKey() {
            goldKey = true;
            statusChanged.Invoke(this);
        }


        /**
         * Collect a silver key.
         */
        public void CollectSilverKey() {
            silverKey = true;
            statusChanged.Invoke(this);
        }


        /**
         * Refill the health reserve fully.
         */
        public bool RefillHealth() {
            bool canRefill = (healthPoints < healthSlots);

            if (canRefill == true) {
                healthPoints = healthSlots;
                statusChanged.Invoke(this);
            }

            return canRefill;
        }


        /**
         * Refill the shield reserve fully.
         */
        public bool RefillShield() {
            bool canRefill = (shieldPoints < shieldSlots);

            if (canRefill == true) {
                shieldPoints = shieldSlots;
                statusChanged.Invoke(this);
            }

            return canRefill;
        }


        /**
         * Refill the water tank fully.
         */
        public bool RefillWater() {
            bool canRefill = (waterDrops < waterSlots);

            if (canRefill == true) {
                waterDrops = waterSlots;
                statusChanged.Invoke(this);
            }

            return canRefill;
        }


        /**
         * Decrease the water tank by one drop.
         */
        public bool DecreaseWater() {
            bool canDecrease = (waterDrops > 0);

            if (canDecrease == true) {
                waterDrops -= 1;
                statusChanged.Invoke(this);
            }

            return canDecrease;
        }


        /**
         * Refill the water tank by one drop.
         */
        public bool InreaseWater() {
            bool canIncrease = (waterDrops < waterSlots);

            if (canIncrease == true) {
                waterDrops += 1;
                statusChanged.Invoke(this);
            }

            return canIncrease;
        }


        /**
         * Decrease the player's shield and health.
         */
        public bool DamagePlayer() {
            if (shieldPoints == 0) {
                healthPoints -= 2;
                statusChanged.Invoke(this);
            } else if (healthPoints > 0) {
                healthPoints -= 1;
                shieldPoints -= 1;
                statusChanged.Invoke(this);
            }

            return healthPoints > 0;
        }
    }
}
