using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Plays music when the game starts.
     */
    public class OnGameStart: MonoBehaviour {
        private void Start() {
            AudioService.PlayLoop(gameObject, "Game Music");
        }
    }
}
