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
            EmableMonsterPhysics(monster);
        }


        /**
         * Enables the physics of the monster when dead.
         */
        private void EmableMonsterPhysics(MonsterController monster) {
            CapsuleCollider collider = monster.GetComponent<CapsuleCollider>();
            Rigidbody body = monster.GetComponent<Rigidbody>();
            Vector3 center = collider.center;

            collider.center = new Vector3(center.x, 0.4f, center.z);
            body.isKinematic = false;
        }
    }
}
