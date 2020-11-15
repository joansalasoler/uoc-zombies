using UnityEngine;

namespace Game.Shared {

    /**
     * Monster state interface.
     */
    public abstract class MonsterState {

        /** Invoked on state enter */
        public virtual void OnStateEnter(MonsterController monster) {}

        /** Invoked on state exit */
        public virtual void OnStateExit(MonsterController monster) {}

        /** Invoked each frame update */
        public virtual void OnUpdate(MonsterController monster) {}

        /** Invoked on each physics update */
        public virtual void OnFixedUpdate(MonsterController monster) {}

        /** Trigger enter event on the monster */
        public virtual void OnTriggerEnter(MonsterController monster, Collider collider) {}

        /** Trigger stay event on the monster */
        public virtual void OnTriggerStay(MonsterController monster, Collider collider) {}

        /** Trigger leave event on the monster */
        public virtual void OnTriggerExit(MonsterController monster, Collider collider) {}
    }
}
