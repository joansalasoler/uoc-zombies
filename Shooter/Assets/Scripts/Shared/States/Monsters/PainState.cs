using UnityEngine;

namespace Game.Shared {

    /**
     * A monster was damaged by the player.
     */
    public class PainState : MonsterState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            AudioService.PlayOneShot(monster.gameObject, "Damage Monster");
            monster.animator.SetTrigger("damage");
            monster.context.SetState(monster.context.ALERT);
        }
    }
}
