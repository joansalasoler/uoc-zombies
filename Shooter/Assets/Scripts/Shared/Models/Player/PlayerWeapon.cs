using System;
using System.ComponentModel;
using UnityEngine;

namespace Game.Shared {

    /**
     * Properties of a player weapon.
     */
    [CreateAssetMenu]
    public class PlayerWeapon : ScriptableObject {

        /** Weapon game object */
        public GameObject prefab = null;

        /** Maximum shooting distance */
        public float distance = 10.0f;
    }
}
