using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * A key on a player's stock.
     */
    [CreateAssetMenu]
    public class StockKey : ScriptableObject {

        /** Number of items */
        public int count;

        /** Icon of this item */
        public Sprite icon;
    }
}
