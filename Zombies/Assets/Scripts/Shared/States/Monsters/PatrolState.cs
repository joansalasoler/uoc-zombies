using UnityEngine;
using System.Collections;

namespace Game.Shared {

    /**
     * A monster is patroling a path.
     */
    public class PatrolState : MonsterState {

        /** Current waypoint direction to follow */
        private Direction direction = Direction.FORWARD;

        /** Current path we are moving on */
        private Waypath waypath = null;

        /** Current waypoint we are moving towards */
        private Waypoint waypoint = null;

        /** Animation loop */
        private IEnumerator animation = null;

        /** Maximum seconds the monster can stay without moving */
        private float maxStuckTime = 5.0f;

        /** Last time the monser's waypoint was changed */
        private float lastChangeTime = 0.0f;


        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            AudioService.PlayLoop(monster.gameObject, "Monster Walk");

            waypath = waypath ?? monster.waypath;
            waypoint = waypath.ClosestPoint(monster.transform.position);
            monster.MoveTowards(waypoint.transform.position);
            lastChangeTime = Time.time;

            animation = AnimatePatrol(monster);
            monster.StartCoroutine(animation);
        }


        /**
         * Invoked when this state is deactivated.
         */
        public override void OnStateExit(MonsterController monster) {
            AudioService.StopLoop(monster.gameObject);
            monster.StopCoroutine(animation);
        }


        /**
         * Go into alert state when the player enters the action radius.
         */
        public override void OnTriggerEnter(MonsterController monster, Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                monster.context.SetState(monster.context.ALERT);
            }
        }


        /**
         * Go into alert state if the player is on front of the monster.
         */
        public override void OnTriggerStay(MonsterController monster, Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                if (monster.IsPlayerOnSight()) {
                    monster.context.SetState(monster.context.ALERT);
                }
            }
        }


        /**
         * Move to the next waypoint when a target is reached.
         */
        public override void OnUpdate(MonsterController monster) {
            if (monster.isAlive == false) {
                return;
            }

            if (IsStuckWithoutMoving(monster)) {
                direction = (Direction) (-((int) direction));
            } else if (!monster.IsAtWaypoint()) {
                return;
            }

            if (waypoint.IsIntersection()) {
                waypath = waypoint.RandomPath();
            }

            if (!waypath.HasNextPoint(direction, waypoint)) {
                direction = (Direction) (-((int) direction));
            }

            waypoint = waypath.NextPoint(direction, waypoint);
            monster.MoveTowards(waypoint.transform.position);
            lastChangeTime = Time.time;
        }


        /**
         * Check if the monster has been stuck without moving.
         */
        private bool IsStuckWithoutMoving(MonsterController monster) {
            bool isTimeElapsed = (Time.time - lastChangeTime >= maxStuckTime);
            bool isSlowedDown = monster.navigator.velocity.magnitude < 0.5f;
            bool isPending = monster.navigator.pathPending;

            return isTimeElapsed && !isPending && isSlowedDown;
        }


        /**
         * Plays animations randomly while the monster walks.
         */
        private IEnumerator AnimatePatrol(MonsterController monster) {
            while (monster.gameObject.activeSelf) {
                yield return new WaitForSeconds(Random.Range(5.0f, 20.0f));
                monster.animator.SetTrigger("search");
            }
        }
    }
}
