using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Game.Shared {

    /**
     * Generic controller for monsters.
     */
    public class MonsterController : ActorController {

        /** Monster animator reference */
        [SerializeField] public Animator animator;

        /** Monster mesh agent reference */
        [SerializeField] public NavMeshAgent navigator;

        /** Current patrol waypath of the monster */
        [SerializeField] public Waypath waypath;

        /** Current state of the monser */
        private MonsterState state = MonsterState.WAIT;


        /**
         * Checks if a monster is currently at a waypoint.
         */
        public bool IsAtWaypoint() {
            return navigator.remainingDistance <= navigator.stoppingDistance;
        }


        /**
         * Makes the monster move torwards a transform.
         */
        public void MoveTowards(Vector3 position) {
            navigator.destination = position;
        }


        /**
         * Damage this actor (kills it by default).
         */
        public override void Damage() {
            AudioService.PlayOneShot(gameObject, "Damage Monster");
            animator.SetTrigger("damage");
            base.Damage();
        }


        /**
         * Sets a new state for the monster.
         */
        public void SetState(MonsterState state) {
            this.state.OnStateExit(this);
            this.state = state;
            this.state.OnStateEnter(this);
        }


        /**
         * State initialization.
         */
        private void Start() {
            SetState(MonsterState.WAIT);
            AudioService.PlayLoop(gameObject, "Monster Walk");
        }


        /**
         * Invoked on each frame update.
         */
        private void Update() {
            state.OnUpdate(this);
        }


        /**
         * An object entered this monster's action radius.
         */
        private void OnTriggerEnter(Collider collider) {
            state.OnTriggerEnter(this, collider);
        }


        /**
         * An object is on this monster's action radius.
         */
        private void OnTriggerStay(Collider collider) {
            state.OnTriggerStay(this, collider);
        }


        /**
         * An object left this monster's action radius.
         */
        private void OnTriggerExit(Collider collider) {
            state.OnTriggerExit(this, collider);
        }
    }
}
