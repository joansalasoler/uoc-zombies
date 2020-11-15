using UnityEngine;

namespace Game.Shared {

    /**
     * A monster was alerted by the player presence.
     */
    public class AlertState : MonsterState {

        /** Wether the player is still on the action radius */
        private bool playerIsNear = false;


        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            monster.LookTowards(monster.player.transform.position);
            monster.StopMoving();
        }


        /**
         * Invoked when this state is deactivated.
         */
        public override void OnStateExit(MonsterController monster) {
            monster.StopLooking();
        }


        /**
         * Check if the player is near.
         */
        public override void OnTriggerEnter(MonsterController monster, Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                playerIsNear = true;
            }
        }


        /**
         * Check if the player is not near.
         */
        public override void OnTriggerExit(MonsterController monster, Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                playerIsNear = false;
            }
        }


        /**
         * Search for a player and change to the attack state if found.
         */
        public override void OnUpdate(MonsterController monster) {
            if (monster.IsLookingAtTarget()) {
                bool playerOnSight = monster.IsPlayerOnSight();
                monster.StopLooking();

                if (playerOnSight && monster.isRunner) {
                    monster.context.SetState(monster.context.PANIC);
                } else if (playerIsNear && playerOnSight) {
                    monster.context.SetState(monster.context.ATTACK);
                } else {
                    monster.context.SetState(monster.context.PATROL);
                }
            }
        }
    }
}
