using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Game.Shared {

    /**
     * Generic controller for dragons.
     */
    public class DragonController : ActorController {

        /** Monster heard the player */
        public readonly ActorState ALERT = new AlertState();

        /** Monster was murdered by the player */
        public readonly ActorState DIE = new DieState();

        /** Monster was damaged by the player */
        public readonly ActorState PAIN = new PainState();

        /** Monster is running away from the player */
        public readonly ActorState PANIC = new PanicState();

        /** Monster is moving around the scene */
        public readonly ActorState PATROL = new PatrolState();

        /** Monster is waiting for commands */
        public readonly ActorState WAIT = new WaitState();

        /** Layers affected by dragon raycasts */
        [HideInInspector] public LayerMask layerMask;

        /** If dragon raycasts must hit triggers */
        [HideInInspector] public QueryTriggerInteraction hitTriggers;

        /** Current player reference */
        [HideInInspector] public PlayerController player = null;

        /** Monster animator reference */
        public Animator animator;

        /** Monster mesh agent reference */
        public NavMeshAgent navigator;

        /** Current patrol waypath of the dragon */
        public Waypath waypath;

        /** Reward for killing the dragon */
        public GameObject rewardPrefab = null;

        /** Number of hits the dragon takes before dying */
        public int healthPoints = 1;

        /** Height at which the character has the eyes */
        public float eyesHeight = 1.5f;

        /** Distance at which a player may be detected */
        public float sightRadius = 30.0f;

        /** Angle at which a player may be detected */
        public float sightAngle = 50.0f;

        /** Rotation speed in radians per second */
        public float rotationSpeed = 6.0f;

        /** Shooting speed in seconds */
        public float attackSpeed = 1.5f;

        /** Position torwards which to roate */
        private Vector3 lookAtTarget;

        /** If dragon must rotate towards a target */
        private bool lookAtIsActive = false;


        /**
         * State initialization.
         */
        private void Start() {
            GameObject playerObject = GameObject.FindWithTag("Player");
            player = playerObject.GetComponent<PlayerController>();
            hitTriggers = QueryTriggerInteraction.Ignore;
            layerMask = GetRaycastLayerMask();
            SetState(WAIT);
        }


        /**
         * Checks if a dragon is currently at a waypoint.
         */
        public bool IsAtWaypoint() {
            if (navigator.enabled == false) {
                return false;
            }

            bool isPending = navigator.pathPending;
            bool isAtPoint = navigator.remainingDistance <= navigator.stoppingDistance;

            return isAtPoint && !isPending;
        }


        /**
         * Check if the dragon is rotated toward the look at target.
         */
        public bool IsLookingAtTarget() {
            return Vector3.Angle(lookAtTarget, transform.forward) < 5.0f;
        }


        /**
         * Check if the player is visible on front of the dragon.
         *
         * That is, if on front of the dragon inside the angle of vision and a
         * ray can be traced from the monser's eyes to the player.
         */
        public bool IsPlayerOnSight() {
            Vector3 target = player.transform.position - transform.position;
            float angle = Vector3.Angle(target, transform.forward);

            if (angle > sightAngle) {
                return false;
            }

            RaycastHit hit;

            Vector3 origin = transform.position;
            Vector3 position = eyesHeight * Vector3.up + origin;
            Vector3 direction = -eyesHeight * Vector3.up + target;
            Ray ray = new Ray(position, direction);

            Debug.DrawRay(position, direction);

            if (Physics.Raycast(ray, out hit, sightRadius, layerMask, hitTriggers)) {
                if (hit.collider.gameObject.CompareTag("Player")) {
                    return true;
                }
            }

            return false;
        }


        /**
         * Makes the dragon look at a certain point.
         */
        public void LookTowards(Vector3 position) {
            Vector3 target = position - transform.position;
            lookAtTarget = new Vector3(target.x, 0, target.z);
            lookAtIsActive = true;
        }


        /**
         * Makes the dragon stop rotating towards a target.
         */
        public void StopLooking() {
            lookAtIsActive = false;
        }


        /**
         * Makes the dragon move torwards a transform.
         */
        public void MoveTowards(Vector3 position) {
            if (navigator.enabled) {
                navigator.SetDestination(position);
                navigator.isStopped = false;
            }
        }


        /**
         * Makes the dragon stop from moving.
         */
        public void StopMoving() {
            if (navigator.enabled) {
                navigator.isStopped = true;
            }
        }


        /**
         * Damage this dragon.
         */
        public override void Damage() {
            if (isAlive == false) {
                return;
            }

            if (healthPoints > 1) {
                SetState(PAIN);
                healthPoints -= 1;
            } else {
                Kill();
            }
        }


        /**
         * Kill this dragon.
         */
        public override void Kill() {
            if (isAlive) {
                RewardPlayer();
                SetState(DIE);
                healthPoints = 0;
                base.Kill();
            }
        }


        /**
         * Mask to use for the dragon's raycasts.
         */
        public LayerMask GetRaycastLayerMask() {
            int filter = LayerMask.GetMask("Player Dome");
            int mask = Physics.DefaultRaycastLayers & ~filter;

            return mask;
        }


        /**
         * Reward the player for killing this dragon.
         */
        private void RewardPlayer() {
            if (rewardPrefab != null) {
                Vector3 position = transform.position;
                GameObject reward = Instantiate(rewardPrefab);

                position.y += 0.75f;
                reward.transform.position = position;
            }
        }


        /**
         * Invoked on each frame update.
         */
        private void Update() {
            state.OnUpdate(this);

            if (lookAtIsActive == false) {
                return;
            }

            float step = rotationSpeed * Time.deltaTime;

            Vector3 target = lookAtTarget;
            Vector3 current = transform.forward;
            Vector3 rotation = Vector3.RotateTowards(current, target, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(rotation);
        }


        /**
         * Invoked on each physics update.
         */
        private void FixedUpdate() {
            state.OnFixedUpdate(this);
        }


        /**
         * An object entered this dragon's action radius.
         */
        private void OnTriggerEnter(Collider collider) {
            state.OnTriggerEnter(this, collider);
        }


        /**
         * An object is on this dragon's action radius.
         */
        private void OnTriggerStay(Collider collider) {
            state.OnTriggerStay(this, collider);
        }


        /**
         * An object left this dragon's action radius.
         */
        private void OnTriggerExit(Collider collider) {
            state.OnTriggerExit(this, collider);
        }
    }
}
