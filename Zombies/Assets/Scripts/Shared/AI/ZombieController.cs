using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Shared {

    /**
     * Controller for zombies.
     */
    public class ZombieController : ActorController {

        /** Navigating from on waypoint to another */
        public PatrolState PatrolState = new PatrolState();


        /**
         * Initialization.
         */
        private void Start() {
            SetState(PatrolState);
        }
    }
}
