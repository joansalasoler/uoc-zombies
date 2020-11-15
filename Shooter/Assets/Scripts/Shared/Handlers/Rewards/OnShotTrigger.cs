using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Rewards the player when an object is shot.
     */
    public class OnShotTrigger: MonoBehaviour {

        /** Reward template */
        [SerializeField] private GameObject rewardPrefab = null;


        /**
         * Fill the player's shield reserve if empty.
         */
        public void OnShotTriggerEnter(Collider collider) {
            if (rewardPrefab != null) {
                RewardPlayer();
            }
        }


        /**
         * Instantiate the reward.
         */
        private void RewardPlayer() {
            GameObject reward = Instantiate(rewardPrefab);
            Vector3 position = transform.position + 0.75f * Vector3.up;
            reward.transform.position = position;
            rewardPrefab = null;
        }
    }
}
