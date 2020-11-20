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
            monster.StopMoving();
            AudioService.PlayOneShot(monster.gameObject, "Monster Die");
            monster.animator.SetTrigger("die");
            monster.navigator.enabled = false;
        }
    }
}
