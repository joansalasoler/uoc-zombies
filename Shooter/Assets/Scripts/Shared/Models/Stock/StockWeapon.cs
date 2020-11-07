using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * A weapon item on a player's stock.
     */
    [CreateAssetMenu]
    public class StockWeapon : ScriptableObject {

        /** Number of items */
        public int count;

        /** Icon of this item */
        public Sprite icon;
    }
}
