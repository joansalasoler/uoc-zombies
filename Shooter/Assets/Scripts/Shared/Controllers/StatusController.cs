using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Shared;


/**
 * Game status overlay controller.
 */
public class StatusController : MonoBehaviour {

    /** Player health indicator */
    public HealthIndicator health = null;

    /** Current player controller */
    private PlayerController player = null;


    /**
     * Initialize this overlay.
     */
    public void Start() {
        if (player == null) {
            GameObject o = GameObject.FindWithTag("Player");
            player = o.GetComponentInChildren<PlayerController>();
        }
    }
}
