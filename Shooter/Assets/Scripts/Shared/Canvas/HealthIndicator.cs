using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Game.Shared {

    /**
     * Shows life and shield indicators for a player.
     */
    public class HealthIndicator : MonoBehaviour {

        /** Life indicator object */
        [SerializeField] private GameObject life = null;

        /** Shield indicator object */
        [SerializeField] private GameObject shield = null;


        /**
         * Sets the displayed life points.
         */
        public void SetLifePoints(int points) {
            ResizeIndicator(life, points);
        }


        /**
         * Sets the displayed shield points.
         */
        public void SetShieldPoints(int points) {
            ResizeIndicator(shield, points);
        }


        /**
         * Resize the given indicator to match a number of points.
         */
        private void ResizeIndicator(GameObject o, int points) {
            var sprite = o.GetComponent<SpriteRenderer>();
            var transform = o.GetComponent<RectTransform>();
            var parent = gameObject.GetComponent<RectTransform>();

            sprite.size = new Vector2(5.0f * points, 11.0f);
            transform.sizeDelta = new Vector2(5.0f * points, 11.0f);
            LayoutRebuilder.ForceRebuildLayoutImmediate(parent);
        }
    }
}
