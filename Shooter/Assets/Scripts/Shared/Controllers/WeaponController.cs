using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for the player weapons. Instantiates the set of available
     * weapons and allows toggling between them.
     */
    public class WeaponController : MonoBehaviour {

        /** If shots must hit triggers */
        [HideInInspector] public QueryTriggerInteraction hitTriggers;

        /** Layers affected by player shoots */
        [HideInInspector] public LayerMask layerMask;

        /** Active weapon or null while rearming */
        [HideInInspector] public PlayerWeapon activeWeapon = null;

        /** Available weapons for the player */
        [SerializeField] private List<PlayerWeapon> weapons = null;

        /** Weapon game object instances */
        private List<GameObject> instances = null;

        /** Weapon animator instance */
        private Animator animator = null;

        /** Center of the camera */
        private Vector3 center = new Vector3(0.5f, 0.5f, 0.0f);

        /** Last time the weapon was shot */
        private float lastShotTime = 0.0f;

        /** Active weapon index */
        private int activeIndex = -1;


        /**
         * Switch to the first weapon.
         */
        private void Start() {
            ToggleWeapon();
        }


        /**
         * Instantiate the weapons.
         */
        private void Awake() {
            hitTriggers = QueryTriggerInteraction.Ignore;
            layerMask = GetShootLayerMask();
            animator = GetComponent<Animator>();
            instances = new List<GameObject>();

            foreach (PlayerWeapon weapon in weapons) {
                GameObject prefab = weapon.weaponPrefab;
                instances.Add(Instantiate(prefab, transform));
            }
        }


        /**
         * Check if a weapon is currently active.
         */
        public bool HasActiveWeapon() {
            return activeWeapon != null;
        }


        /**
         * Sets the speed at which the weapons move.
         */
        public void SetSpeed(float speed) {
            animator.SetFloat("speed", speed);
        }


        /**
         * Switch to the next weapon.
         */
        public void ToggleWeapon() {
            animator.SetBool("rearming", true);
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
         * Checks if the weapon can be shot.
         */
        public bool CanShootWeapon() {
            bool canShoot = false;

            if (animator.GetBool("shooting")) {
                canShoot = false;
            } else if (HasActiveWeapon()) {
                float rateOfFire = activeWeapon.rateOfFire;
                float timeSinceShot = Time.time - lastShotTime;
                canShoot = rateOfFire < timeSinceShot;
            }

            return canShoot;
        }


        /**
         * Shoots the active weapon from the center of the camera.
         */
        public bool ShootWeapon() {
            if (CanShootWeapon() == false) {
                return false;
            }

            animator.SetBool("shooting", true);
            Invoke("OnShootFinished", 0.35f);

            RaycastHit hit;
            Vector3 deviaiton = GetShootDeviation();
            Ray ray = GetShootOriginRay(center + deviaiton);
            float distance = activeWeapon.shootDistance;

            if (Physics.Raycast(ray, out hit, distance, layerMask, hitTriggers)) {
                EmbedImpactDecal(hit);
                lastShotTime = Time.time;
            }

            return true;
        }


        /**
         * Origin and direction of the shoots.
         */
        public Ray GetShootOriginRay(Vector3 center) {
            return Camera.main.ViewportPointToRay(center);
        }


        /**
         * Deviation direction of the weapon.
         */
        public Vector3 GetShootDeviation() {
            Vector3 random = Random.insideUnitCircle.normalized;
            Vector3 deviation = activeWeapon.deviation * random;

            return deviation;
        }


        /**
         * Mask to use for the weapon shooting raycasts.
         */
        public LayerMask GetShootLayerMask() {
            int filter = LayerMask.GetMask("Player Dome", "Monster Dome");
            int mask = Physics.DefaultRaycastLayers & ~filter;

            return mask;
        }


        /**
         * Embed an impact decal into a hit position.
         */
        private void EmbedImpactDecal(RaycastHit hit) {
            Vector3 position = 0.01f * hit.normal + hit.point;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            Instantiate(activeWeapon.impactPrefab, position, rotation);
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
            lastShotTime = 0.0f;
            activeWeapon = weapons[activeIndex];
            animator.SetBool("rearming", false);
        }


        /**
         * Invoked to clear the shooting animation.
         */
        private void OnShootFinished() {
            animator.SetBool("shooting", false);
        }
    }
}
