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


        /**
         * Invoked when this state is activated.
         */
        public override void OnStateEnter(MonsterController monster) {
            waypath = waypath ?? monster.waypath;
            waypoint = waypath.ClosestPoint(monster.transform.position);
            monster.MoveTowards(waypoint.transform.position);

            animation = AnimatePatrol(monster);
            monster.StartCoroutine(animation);
        }


        /**
         * Invoked when this state is deactivated.
         */
        public override void OnStateExit(MonsterController monster) {
            monster.StopCoroutine(animation);
        }


        /**
         * Move to the next waypoint when a target is reached.
         */
        public override void OnUpdate(MonsterController monster) {
            if (monster.IsAtWaypoint() == false) {
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
