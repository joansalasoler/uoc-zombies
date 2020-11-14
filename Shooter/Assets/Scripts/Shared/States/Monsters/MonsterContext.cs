using UnityEngine;

namespace Game.Shared {

    /**
     * Represents a monster state.
     */
    public class MonsterContext {

        /** Monster heard the player */
        public readonly MonsterState ALERT = new AlertState();

        /** Monster is attacking the player */
        public readonly MonsterState ATTACK = new AttackState();

        /** Monster was murdered by the player */
        public readonly MonsterState DIE = new DieState();

        /** Monster was damaged by the player */
        public readonly MonsterState PAIN = new PainState();

        /** Monster is running away from the player */
        public readonly MonsterState PANIC = new PanicState();

        /** Monster is moving around the scene */
        public readonly MonsterState PATROL = new PatrolState();

        /** Monster is waiting for commands */
        public readonly MonsterState WAIT = new WaitState();

        /** Controller for this context */
        private MonsterController monster = null;

        /** Current state of the monser */
        private MonsterState state = null;


        /**
         * Create a new context.
         */
        public MonsterContext(MonsterController monster) {
            this.monster = monster;
            this.state = WAIT;
        }


        /**
         * Get the current state.
         */
        public MonsterState GetState() {
            return this.state;
        }


        /**
         * Sets a new state for the monster.
         */
        public void SetState(MonsterState state) {
            Debug.Log($"{monster.name} has state {state}");
            this.state.OnStateExit(monster);
            this.state = state;
            this.state.OnStateEnter(monster);
        }
    }
}
