using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Shared {

    /**
     * Synchronize the agent movement with the animation.
     */
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieLocomotion: MonoBehaviour {

        /** Humanoid character animator */
        private Animator animator = null;

        /** Navigation agent reference */
        private NavMeshAgent agent = null;

        /** Speed at which the agent is moving */
        private float speed = 0.0f;


        /**
         * Initialization.
         */
        private void Start() {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            agent.updatePosition = false;
            agent.updateRotation = true;
            speed = agent.speed;
        }


        /**
         * Synchronize the walking animation with the agent.
         */
        private void Update() {
            float magnitude = Vector3.Dot(transform.forward, agent.velocity);
            speed = Mathf.Lerp(speed, magnitude, Time.deltaTime);

            Vector3 offset = speed * agent.velocity.normalized;
            agent.nextPosition = transform.position + offset;

            animator.SetFloat("Speed", speed);
        }


        /**
         * Move the agent with the animation.
         */
        private void OnAnimatorMove() {
            Vector3 position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;
        }
    }
}
