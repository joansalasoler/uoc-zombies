using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * An item on a player's stock.
     */
    [CreateAssetMenu]
    public class StockItem : ScriptableObject {

        /** Number of items */
        public int count;

        /** Icon of this item */
        public Sprite icon;
    }
}
