using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for the player weapons. Instantiates the set of available
     * weapons and allows toggling between them.
     */
    public class WeaponController : MonoBehaviour {

        /** Available weapons for the player */
        [SerializeField] private List<PlayerWeapon> weapons = null;

        /** Active weapon or null while rearming */
        [HideInInspector] public PlayerWeapon activeWeapon = null;

        /** If shots must hit triggers */
        [HideInInspector] public QueryTriggerInteraction hitTriggers;

        /** Layers affected by player shoots */
        [HideInInspector] public LayerMask layerMask;

        /** Force of the water impactin with objects */
        public float pushForce = 100.0f;

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
         * Sets the speed at which the weapons move.
         */
        public void SetSpeed(float speed) {
            animator.SetFloat("speed", speed);
        }


        /**
         * Sets if the player is still alive.
         */
        public void SetAliveState(bool alive) {
            animator.SetBool("isAlive", alive);
        }


        /**
         * Check if a weapon is currently active.
         */
        public bool HasActiveWeapon() {
            return activeWeapon != null;
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
            return HasActiveWeapon() && IsInShotRate();
        }


        /**
         * Checks if the rate of fire period elapsed.
         */
        public bool IsInShotRate() {
            float rateOfFire = activeWeapon.rateOfFire;
            float timeSinceShot = Time.time - lastShotTime;
            bool inShotRate = rateOfFire < timeSinceShot;

            return inShotRate;
        }


        /**
         * Animate the gun and clear the last shot time.
         */
        public void ShootNothing() {
            AudioService.PlayOneShot(gameObject, "Shot Blocked");
            animator.SetTrigger("shoot");
            lastShotTime = Time.time;
        }


        /**
         * Animate the gun and clear the last shot time.
         */
        public void ShootWater() {
            AudioService.PlayOneShot(gameObject, "Weapon Shot");
            animator.SetTrigger("shoot");
            lastShotTime = Time.time;
        }


        /**
         * Shoots the active weapon from the center of the camera.
         */
        public bool ShootWeapon() {
            if (CanShootWeapon() == false) {
                return false;
            }

            RaycastHit hit;
            Vector3 deviaiton = GetShootDeviation();
            Ray ray = GetShootOriginRay(center + deviaiton);
            float distance = activeWeapon.shootDistance;

            if (Physics.Raycast(ray, out hit, distance, layerMask, hitTriggers)) {
                if (hit.collider.CompareTag("Monster")) {
                    hit.collider.GetComponent<ActorController>().Damage();
                } else {
                    if (!hit.collider.CompareTag("Player")) {
                        EmbedImpactDecal(hit);
                    }

                    if (hit.collider.CompareTag("Moveable")) {
                        PushColliderBody(hit);
                    }
                }
            }

            ShootWater();

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
            int filter = LayerMask.GetMask("Player", "Player Dome", "Monster Dome");
            int mask = Physics.DefaultRaycastLayers & ~filter;

            return mask;
        }


        /**
         * Push a moveable object if it was shot.
         */
        private void PushColliderBody(RaycastHit hit) {
            Vector3 force = pushForce * Vector3.one;
            Rigidbody body = hit.collider.attachedRigidbody;
            body.AddForceAtPosition(force, hit.point);
        }


        /**
         * Embed an impact decal into a hit position.
         */
        private void EmbedImpactDecal(RaycastHit hit) {
            Vector3 position = 0.01f * hit.normal + hit.point;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, -hit.normal);
            GameObject prefab = activeWeapon.impactPrefab;
            GameObject decal = Instantiate(prefab, position, rotation, hit.transform);

            Vector3 s = hit.transform.lossyScale;
            decal.transform.localScale = Vector3.one * (1f / s.x);
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
    }
}
