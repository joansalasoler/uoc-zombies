using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Game.Shared {

    /**
     * Generic controller for monsters.
     */
    public class MonsterController : ActorController {

        /** Current player reference */
        [HideInInspector] public PlayerController player = null;

        /** State context of the monster */
        [HideInInspector] public MonsterContext context = null;

        /** Monster animator reference */
        public Animator animator;

        /** Monster mesh agent reference */
        public NavMeshAgent navigator;

        /** Current patrol waypath of the monster */
        public Waypath waypath;

        /** Reward for killing the monster */
        public GameObject rewardPrefab = null;

        /** Projectile that the monster shots */
        public GameObject projectilePrefab = null;

        /** Wether the monster will panic when alerted */
        public bool isRunner = false;

        /** Number of hits the monster takes before dying */
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

        /** If monster must rotate towards a target */
        private bool lookAtIsActive = false;


        /**
         * State initialization.
         */
        private void Start() {
            GameObject playerObject = GameObject.FindWithTag("Player");
            player = playerObject.GetComponent<PlayerController>();

            context = new MonsterContext(this);
            context.SetState(context.WAIT);
        }


        /**
         * Checks if a monster is currently at a waypoint.
         */
        public bool IsAtWaypoint() {
            return navigator.remainingDistance <= navigator.stoppingDistance;
        }


        /**
         * Check if the monster is rotated toward the look at target.
         */
        public bool IsLookingAtTarget() {
            return Vector3.Angle(lookAtTarget, transform.forward) < 5.0f;
        }


        /**
         * Check if the player is visible on front of the monster.
         *
         * That is, if on front of the monster inside the angle of vision and a
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

            Debug.DrawRay(position, direction);

            if (Physics.Raycast(position, direction, out hit, sightRadius)) {
                if (hit.collider.gameObject.CompareTag("Player")) {
                    return true;
                }
            }

            return false;
        }


        /**
         * Makes the monster look at a certain point.
         */
        public void LookTowards(Vector3 position) {
            Vector3 target = position - transform.position;
            lookAtTarget = new Vector3(target.x, 0, target.z);
            lookAtIsActive = true;
        }


        /**
         * Makes the monster stop rotating towards a target.
         */
        public void StopLooking() {
            lookAtIsActive = false;
        }


        /**
         * Makes the monster move torwards a transform.
         */
        public void MoveTowards(Vector3 position) {
            if (navigator.enabled) {
                navigator.destination = position;
                navigator.isStopped = false;
            }
        }


        /**
         * Makes the monster stop from moving.
         */
        public void StopMoving() {
            if (navigator.enabled) {
                navigator.isStopped = true;
            }
        }


        /**
         * Damage this monster.
         */
        public override void Damage() {
            if (isAlive == false) {
                return;
            }

            if (healthPoints < 2 && rewardPrefab != null) {
                isRunner = true;
            }

            if (healthPoints > 1) {
                context.SetState(context.PAIN);
                healthPoints -= 1;
            } else {
                Kill();
            }
        }


        /**
         * Kill this monster.
         */
        public override void Kill() {
            if (isAlive) {
                RewardPlayer();
                context.SetState(context.DIE);
                healthPoints = 0;
                base.Kill();
            }
        }


        /**
         * Shoot a projectile towards the player's camera.
         */
        public void ShootAtPlayer() {
            var projectile = Instantiate(projectilePrefab);
            var controller = projectile.GetComponent<ProjectileController>();

            Vector3 height = (eyesHeight - 0.2f) * Vector3.up;
            projectile.transform.position = height + transform.position;
            controller.MoveTowards(Camera.main.transform.position);
        }


        /**
         * Reward the player for killing this monster.
         */
        private void RewardPlayer() {
            if (rewardPrefab != null) {
                Vector3 position = transform.position;
                GameObject reward = Instantiate(rewardPrefab);

                position.x += 1.75f;
                position.y += 0.75f;
                reward.transform.position = position;
            }
        }


        /**
         * Invoked on each frame update.
         */
        private void Update() {
            context.GetState().OnUpdate(this);

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
            context.GetState().OnFixedUpdate(this);
        }


        /**
         * An object entered this monster's action radius.
         */
        private void OnTriggerEnter(Collider collider) {
            context.GetState().OnTriggerEnter(this, collider);
        }


        /**
         * An object is on this monster's action radius.
         */
        private void OnTriggerStay(Collider collider) {
            context.GetState().OnTriggerStay(this, collider);
        }


        /**
         * An object left this monster's action radius.
         */
        private void OnTriggerExit(Collider collider) {
            context.GetState().OnTriggerExit(this, collider);
        }
    }
}
