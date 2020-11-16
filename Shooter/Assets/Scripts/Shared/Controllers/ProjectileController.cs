using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for the monster's projectiles.
     */
    public class ProjectileController : MonoBehaviour {

        /** Target position */
        private Vector3 target = Vector3.zero;

        /** Speed of the projectile */
        public float speed = 20.0f;


        /**
         * Damage this actor (kills it by default).
         */
        public void MoveTowards(Vector3 target) {
            this.target = target;
            Destroy(gameObject, 2.0f);
        }


        /**
         * Move the projectile towards the target.
         */
        private void Update() {
            if (target != Vector3.zero) {
                float step = speed * Time.deltaTime;
                Vector3 origin = transform.position;
                transform.position = Vector3.Slerp(origin, target, step);

                if (Vector3.Distance(origin, target) < 0.001f) {
                    Destroy(gameObject);
                }
            }
        }


        /**
         * Damage the player when impacted by a projectile.
         */
        private void OnTriggerEnter(Collider collider) {
            Destroy(gameObject);

            if (collider.gameObject.CompareTag("Player")) {
                PlayerController player = GetPlayerController(collider);
                player.Damage();
            }
        }


        /**
         * Obtain the player's controller from a collider.
         */
        private PlayerController GetPlayerController(Collider collider) {
            return collider.GetComponent<PlayerController>();
        }
    }
}
