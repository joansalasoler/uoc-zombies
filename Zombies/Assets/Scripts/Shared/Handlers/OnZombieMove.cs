using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Shared {

    /**
     *
     */
    public class OnZombieMove: MonoBehaviour {

        [SerializeField] private Animator animator = null;

        [SerializeField] private NavMeshAgent agent = null;

        float speed = 0.0f;
        float cspeed = 0.0f;
        Quaternion rotation;

        private void Start() {
            agent.updatePosition = false;
            // agent.updateRotation = false;
            speed = agent.speed;
            cspeed = agent.speed;
            rotation = transform.rotation;
        }


        void Update ()
        {
            bool shouldMove = true;
            speed = Mathf.Lerp(speed, agent.velocity.magnitude, Time.deltaTime);

            animator.SetBool("Walk", shouldMove);
            animator.SetFloat ("Speed", speed);

            // Debug.Log($"v = {agent.velocity.magnitude}, s = {speed}");

            // if (agent.velocity.magnitude > agent.radius)
            agent.nextPosition = transform.position + agent.velocity;
        }


        /**
         *
         */
        private void OnAnimatorMove() {
            transform.position = animator.rootPosition;
        }
    }
}
