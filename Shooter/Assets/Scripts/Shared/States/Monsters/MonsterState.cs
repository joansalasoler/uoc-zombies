using UnityEngine;

namespace Game.Shared {

    /**
     * Represents a monster state.
     */
    public abstract class MonsterState {

        /** Monster heard the player */
        public static readonly MonsterState ALERT = new AlertState();

        /** Monster is attacking the player */
        public static readonly MonsterState ATTACK = new AttackState();

        /** Monster is moving around the scene */
        public static readonly MonsterState PATROL = new PatrolState();

        /** Monster is waiting for commands */
        public static readonly MonsterState WAIT = new WaitState();

        /** Invoked on state enter */
        public virtual void OnStateEnter(MonsterController monster) {}

        /** Invoked on state exit */
        public virtual void OnStateExit(MonsterController monster) {}

        /** Invoked each frame update */
        public virtual void OnUpdate(MonsterController monster) {}

        /** Collision enter event on the monster */
        public virtual void OnTriggerEnter(MonsterController monster, Collider collider) {}

        /** Collision stay event on the monster */
        public virtual void OnTriggerStay(MonsterController monster, Collider collider) {}

        /** Collision leave event on the monster */
        public virtual void OnTriggerExit(MonsterController monster, Collider collider) {}
    }
}
