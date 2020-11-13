using UnityEngine;

namespace Game.Shared {

    /**
     * A monster is waiting.
     */
    public class WaitState : MonsterState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            monster.SetState(MonsterState.PATROL);
        }
    }
}
