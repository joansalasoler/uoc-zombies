using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for water splashes (weapon impacts).
     */
    public class SplashController : MonoBehaviour {

        /**
         * Set a random scale/rotation and autodestroy it.
         */
        private void OnEnable() {
            transform.localScale = Vector3.one * Random.Range(0.3f, 0.6f);
            transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
            Destroy(gameObject, 10.5f);
        }
    }
}
