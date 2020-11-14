using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Shared;


/**
 * Game scene controller.
 */
public class GameController : MonoBehaviour {

    /** Player instance */
    public PlayerController player;

    /** Pause overlay controller */
    public PauseController pause;

    /** Game over overlay controller */
    public EndgameController endgame;


    /**
     * Initialize the game.
     */
    private void Start() {
        player.status.Reset();
    }


    /**
     * Loads the main menu scene.
     */
    public void LoadMainScene() {
        SceneManager.LoadScene("Main");
    }


    /**
     * Shows the congratulations overlay.
     */
    public void ShowCongratsOverlay() {
        endgame.ShowCongratsOverlay();
    }


    /**
     * Shows the game over overlay.
     */
    public void ShowGameOverOverlay() {
        endgame.ShowGameOverOverlay();
    }


    /**
     * Attach the events.
     */
    private void OnEnable() {
        player.playerKilled += OnPlayerKilled;
    }


    /**
     * Detach the events.
     */
    private void OnDisable() {
        player.playerKilled -= OnPlayerKilled;
    }


    /**
     * Handle when the player dies.
     */
    public void OnPlayerKilled(PlayerController player) {
        pause.enabled = false;
        player.DisableController();
        Invoke("ShowGameOverOverlay", 3.0f);
        Invoke("LoadMainScene", 6.0f);
    }
}
