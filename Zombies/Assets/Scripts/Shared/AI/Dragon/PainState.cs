using UnityEngine;

namespace Game.Shared {

    /**
     * A actor was damaged by the player.
     */
    public class PainState : ActorState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(ActorController actor) {
            DragonController dragon = (DragonController) actor;
            AudioService.PlayOneShot(actor.gameObject, "Damage Monster");
            dragon.animator.SetTrigger("damage");
            actor.SetState(dragon.ALERT);
        }
    }
}
