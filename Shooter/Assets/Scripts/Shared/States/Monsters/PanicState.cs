using UnityEngine;
using System.Collections;

namespace Game.Shared {

    /**
     * A monster is in panic.
     */
    public class PanicState : MonsterState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            AudioService.PlayLoop(monster.gameObject, "Monster Walk");
            Vector3 origin = monster.transform.position;
            Vector3 direction = monster.transform.forward;
            Vector3 target = -50.0f * direction + origin;

            monster.MoveTowards(target);
            monster.animator.SetTrigger("panic");
            monster.navigator.speed *= 2.0f;
            monster.StartCoroutine(ExitPanicState(monster));
        }


        /**
         * Invoked when this state is deactivated.
         */
        public override void OnStateExit(MonsterController monster) {
            AudioService.StopLoop(monster.gameObject);
            monster.navigator.speed *= 0.5f;
        }


        /**
         * Moves back to the patrol state after 5 seconds.
         */
        private IEnumerator ExitPanicState(MonsterController monster) {
            yield return new WaitForSeconds(5.0f);
            monster.SetState(MonsterState.PATROL);
        }
    }
}
