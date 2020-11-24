using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the actors.
     */
    public class ActorController : MonoBehaviour {

        /** Wether the actor is dead or alive */
        [HideInInspector] public bool isAlive = true;

        /** Current state of the monser */
        [SerializeField] protected ActorState state = null;


        /**
         * Sets a new state for the actor.
         */
        public void SetState(ActorState state) {
            if (this.state != null) {
                this.state.OnStateExit(this);
            }

            if (state != null) {
                this.state = state;
                this.state.OnStateEnter(this);
            }
        }


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
