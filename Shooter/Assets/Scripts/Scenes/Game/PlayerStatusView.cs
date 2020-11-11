using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Shared;


/**
 * Player status overlay controller. Shows the remaining health, shield
 * and water munitions of the player.
 */
public class PlayerStatusView : MonoBehaviour {

    /** Container for health and shield indicators */
    [SerializeField] private Transform healthContainer = null;

    /** Container forwater munition indicators */
    [SerializeField] private Transform waterContainer = null;

    /** Golden key indicator object */
    [SerializeField] private GameObject goldKey = null;

    /** Silver key indicator object */
    [SerializeField] private GameObject silverKey = null;

    /** Health indicator template */
    [SerializeField] private GameObject healthPrefab = null;

    /** Shield indicator template */
    [SerializeField] private GameObject shieldPrefab = null;

    /** Water indicator template */
    [SerializeField] private GameObject waterPrefab = null;

    /** Ordered health indicator controllers */
    private List<StatusIndicator> healthIcons = new List<StatusIndicator>();

    /** Ordered shield indicator controllers */
    private List<StatusIndicator> shieldIcons = new List<StatusIndicator>();

    /** Ordered water indicator controllers */
    private List<StatusIndicator> waterIcons = new List<StatusIndicator>();


    /**
     * Initialize this overlay.
     */
    public void Awake() {
        GameObject o = GameObject.FindWithTag("Player");
        PlayerController player = o.GetComponentInChildren<PlayerController>();
        player.status.statusChanged += OnPlayerStatusChanged;
    }


    /**
     * Update the view when the player status changes.
     */
    private void OnPlayerStatusChanged(PlayerStatus status) {
        CreateWaterIndicators(status);
        CreateHealthIndicators(status);
        CreateShieldIndicators(status);
        UpdateWaterIndicators(status);
        UpdateShieldIndicators(status);
        UpdateHealthIndicators(status);

        goldKey.SetActive(status.goldKey);
        silverKey.SetActive(status.silverKey);
    }


    /**
     * Create the water indicators and add them to the container.
     */
    private void CreateWaterIndicators(PlayerStatus status) {
        while (waterIcons.Count < status.waterSlots) {
            GameObject instance = Instantiate(waterPrefab, waterContainer);
            waterIcons.Add(instance.GetComponent<StatusIndicator>());
        }
    }


    /**
     * Create the shield indicators and add them to the container.
     */
    private void CreateShieldIndicators(PlayerStatus status) {
        while (shieldIcons.Count < status.shieldSlots) {
            GameObject instance = Instantiate(shieldPrefab, healthContainer);
            shieldIcons.Add(instance.GetComponent<StatusIndicator>());
            instance.transform.SetAsFirstSibling();
        }
    }


    /**
     * Create the health indicators and add them to the container.
     */
    private void CreateHealthIndicators(PlayerStatus status) {
        while (healthIcons.Count < status.healthSlots / 2) {
            GameObject instance = Instantiate(healthPrefab, healthContainer);
            healthIcons.Add(instance.GetComponent<StatusIndicator>());
            instance.transform.SetAsFirstSibling();
        }
    }


    /**
     * Update the status of all the water indicators.
     */
    private void UpdateWaterIndicators(PlayerStatus status) {
        for (int i = 0; i < waterIcons.Count; i++) {
            if (i < status.waterDrops) {
                waterIcons[i].ChangeToFull();
            } else {
                waterIcons[i].ChangeToEmpty();
            }
        }
    }


    /**
     * Update the status of all the shield indicators.
     */
    private void UpdateShieldIndicators(PlayerStatus status) {
        for (int i = 0; i < shieldIcons.Count; i++) {
            if (i < status.shieldPoints) {
                shieldIcons[i].ChangeToFull();
            } else {
                shieldIcons[i].ChangeToEmpty();
            }
        }
    }


    /**
     * Update the status of all the health indicators.
     */
    private void UpdateHealthIndicators(PlayerStatus status) {
        for (int i = 0; i < healthIcons.Count; i++) {
            if (i < status.healthPoints / 2) {
                healthIcons[i].ChangeToFull();
            } else if (2 * i < status.healthPoints) {
                healthIcons[i].ChangeToHalf();
            } else {
                healthIcons[i].ChangeToEmpty();
            }
        }
    }
}
