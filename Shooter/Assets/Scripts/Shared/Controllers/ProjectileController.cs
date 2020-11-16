using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for the monster's projectiles.
     */
    public class ProjectileController : MonoBehaviour {

        /** Weapon impact prefab */
        public GameObject impactPrefab = null;

        /** Target position */
        private Vector3 target = Vector3.zero;

        /** Speed of the projectile */
        public float speed = 20.0f;


        /**
         * Damage this actor (kills it by default).
         */
        public void MoveTowards(Vector3 target) {
            this.target = target;
            DestroyProjectile(2.0f);
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
                    DestroyProjectile();
                }
            }
        }


        /**
         * Damage the player when impacted by a projectile.
         */
        private void OnTriggerEnter(Collider collider) {
            DestroyProjectile();

            if (collider.gameObject.CompareTag("Player")) {
                PlayerController player = GetPlayerController(collider);
                player.Damage();
                return;
            }

            RaycastHit hit;
            Vector3 direction = target - transform.position;
            Debug.DrawRay(transform.position, direction, Color.green);

            if (Physics.Raycast(transform.position, direction, out hit)) {
                if (!hit.collider.gameObject.CompareTag("Monster")) {
                    EmbedImpactDecal(hit);
                }
            }
        }


        /**
         * Destroy this projectile's game object.
         */
        private void DestroyProjectile(float seconds = 0.0f) {
            if (gameObject != null) {
                Destroy(gameObject, seconds);
            }
        }


        /**
         * Embed an impact decal into a hit position.
         */
        private void EmbedImpactDecal(RaycastHit hit) {
            Vector3 position = 0.01f * hit.normal + hit.point;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            GameObject decal = Instantiate(impactPrefab, position, rotation, hit.transform);

            Vector3 s = hit.transform.lossyScale;
            decal.transform.localScale = Vector3.one * (1f / s.x);
        }


        /**
         * Obtain the player's controller from a collider.
         */
        private PlayerController GetPlayerController(Collider collider) {
            return collider.GetComponent<PlayerController>();
        }
    }
}
