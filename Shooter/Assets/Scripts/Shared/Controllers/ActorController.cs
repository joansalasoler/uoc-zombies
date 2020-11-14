using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the actors.
     */
    public class ActorController : MonoBehaviour {

        /** Wether the actor is dead or alive */
        [HideInInspector] public bool isAlive = true;


        /**
         * Damage this actor (kills it by default).
         */
        public virtual void Damage() {
            Kill();
        }


        /**
         * Kills this actor.
         */
        public virtual void Kill() {
            isAlive = false;
        }
    }
}
