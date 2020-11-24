using UnityEngine;

namespace Game.Shared {

    /**
     * A dragon is waiting.
     */
    public class WaitState : ActorState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(ActorController actor) {
            DragonController dragon = (DragonController) actor;
            actor.SetState(dragon.PATROL);
        }
    }
}
