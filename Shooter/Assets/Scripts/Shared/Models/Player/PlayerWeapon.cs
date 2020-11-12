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
        public GameObject weaponPrefab = null;

        /** Weapon impact prefab */
        public GameObject impactPrefab = null;

        /** Maximum distance reached by a shoot */
        [Range(1.0f, 100.0f)] public float shootDistance = 10.0f;

        /** Seconds between each shoot */
        [Range(0.35f, 10.0f)] public float rateOfFire = 1.0f;

        /** Deviation of the weapon */
        [Range(0.0f, 1.0f)] public float deviation = 1.0f;
    }
}
