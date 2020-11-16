using UnityEngine;
using System.Collections;

namespace Game.Shared {

    /**
     * A monster is attacking the player.
     */
    public class AttackState : MonsterState {

        /** Attack player corroutine */
        private IEnumerator attacker = null;


        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            attacker = ShootAtPlayer(monster);
            monster.StartCoroutine(attacker);
        }


        /**
         * Invoked when this state is deactivated.
         */
        public override void OnStateExit(MonsterController monster) {
            monster.StopCoroutine(attacker);
            monster.StopLooking();
        }


        /**
         * Collision stay event on the monster.
         */
        public override void OnTriggerStay(MonsterController monster, Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                monster.LookTowards(collider.transform.position);
            }
        }


        /**
         * Collision stay event on the monster.
         */
        public override void OnTriggerExit(MonsterController monster, Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                monster.context.SetState(monster.context.ALERT);
            }
        }


        /**
         * Moves back to the patrol state after 5 seconds.
         */
        private IEnumerator ShootAtPlayer(MonsterController monster) {
            float shootSeconds = monster.attackSpeed - 0.2f;

            while (monster.player.isAlive) {
                monster.animator.SetTrigger("attack");
                yield return new WaitForSeconds(0.2f);

                if (monster.IsPlayerOnSight()) {
                    AudioService.PlayOneShot(monster.gameObject, "Monster Shot");
                    monster.ShootAtPlayer();
                }

                yield return new WaitForSeconds(shootSeconds);
            }
        }
    }
}
