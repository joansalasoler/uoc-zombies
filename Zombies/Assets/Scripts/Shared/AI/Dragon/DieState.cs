using UnityEngine;

namespace Game.Shared {

    /**
     * A actor was killed.
     */
    public class DieState : BaseState {

        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(ActorController actor) {
            DragonController dragon = (DragonController) actor;

            dragon.StopMoving();
            AudioService.PlayOneShot(actor.gameObject, "Monster Die");
            dragon.animator.SetTrigger("die");
            dragon.navigator.enabled = false;
            EmableMonsterPhysics(dragon);
        }


        /**
         * Enables the physics of the actor when dead.
         */
        private void EmableMonsterPhysics(DragonController dragon) {
            CapsuleCollider collider = dragon.GetComponent<CapsuleCollider>();
            Rigidbody body = dragon.GetComponent<Rigidbody>();
            Vector3 center = collider.center;

            collider.center = new Vector3(center.x, 0.35f, center.z);
            body.isKinematic = false;
        }
    }
}
