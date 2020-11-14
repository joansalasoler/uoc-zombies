using UnityEngine;

namespace Game.Shared {

    /**
     * A monster was killed.
     */
    public class DieState : MonsterState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            AudioService.PlayOneShot(monster.gameObject, "Kill Monster");
            monster.animator.SetTrigger("die");
            monster.StopMoving();
        }
    }
}
