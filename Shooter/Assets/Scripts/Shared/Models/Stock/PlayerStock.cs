using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Shared {

    /**
     * Stock of items collected by a player.
     */
    [CreateAssetMenu]
    public class PlayerStock : ScriptableObject {

        /** Item on the player's hands */
        public StockItem handItem = null;

        /** Invoked when the stock changes */
        public static Action<PlayerStock> stockChange;

        /** Collection of items on the stock */
        private HashSet<StockItem> items = new HashSet<StockItem>();


        /**
         * Hold a stock item.
         */
        public void Grab(StockItem item) {
            handItem = item;
            stockChange.Invoke(this);
        }


        /**
         * Store an item into the stock.
         */
        public void Store(StockItem item) {
            items.Add(item);
            stockChange.Invoke(this);
        }


        /**
         * Throw an item from the stock.
         */
        public void Throw(StockItem item) {
            items.Remove(item);
            stockChange.Invoke(this);
        }


        /**
         * Clear this stock.
         */
        public void Clear() {
            items.Clear();
            stockChange.Invoke(this);
        }
    }
}
