using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller of player weapons.
     */
    public class WeaponController : MonoBehaviour {

        /** Active weapon or null while rearming */
        [HideInInspector] public PlayerWeapon activeWeapon = null;

        /** Available weapons for the player */
        public PlayerWeapon[] weapons = null;

        /** Weapon game object instances */
        private List<GameObject> instances = null;

        /** Weapon animator instance */
        private Animator animator = null;

        /** Active weapon index */
        private int activeIndex = -1;


        /**
         * Switch to the next weapon.
         */
        public void ToggleWeapon() {
            animator.SetBool("rearming", true);
        }


        /**
         * Sets the speed at which the weapons move.
         */
        public void SetSpeed(float speed) {
            animator.SetFloat("speed", speed);
        }


        /**
         * Instantiate the weapons.
         */
        private void Awake() {
            instances = new List<GameObject>();
            animator = GetComponent<Animator>();

            foreach (PlayerWeapon weapon in weapons) {
                GameObject prefab = Instantiate(weapon.prefab, transform);
                instances.Add(prefab);
            }
        }


        /**
         * Switch to the first weapon.
         */
        private void Start() {
            ToggleWeapon();
        }


        /**
         * Aram a weapon given its index.
         */
        private void ArmWeapon(int index) {
            instances[index].SetActive(true);
            activeIndex = index;
        }


        /**
         * Disarm the current weapon.
         */
        private void DisarmWeapon() {
            instances[activeIndex].SetActive(false);
            activeIndex = -1;
        }


        /**
         * Invoked by the animator when the current weapon is hidden from
         * view. Disarms the current weapon and arms the next.
         */
        private void OnToggleWeapon() {
            int nextIndex = (1 + activeIndex) % instances.Count;

            if (activeWeapon != null) {
                activeWeapon = null;
                DisarmWeapon();
            }

            ArmWeapon(nextIndex);
        }


        /**
         * Invoked when the rearm animation finishes. This effectively
         * activates the weapon.
         */
        private void OnRearmFinished() {
            activeWeapon = weapons[activeIndex];
            animator.SetBool("rearming", false);
        }
    }
}
