using UnityEngine;

namespace Game.Shared {

    /**
     * A actor was damaged by the player.
     */
    public class PainState : BaseState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(ActorController actor) {
            DragonController dragon = (DragonController) actor;
            AudioService.PlayOneShot(actor.gameObject, "Damage Monster");
            dragon.animator.SetTrigger("damage");
            dragon.SetDragonState(dragon.ALERT);
        }
    }
}
