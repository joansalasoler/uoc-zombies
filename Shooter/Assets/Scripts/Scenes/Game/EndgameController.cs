using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Endgame overlay controller.
 */
public class EndgameController : MonoBehaviour {

    /** Object to show when the player dies */
    [SerializeField] private GameObject gameOver = null;

    /** Object to show when the player wins */
    [SerializeField] private GameObject congrats = null;


    /**
     * Shows the game over overlay.
     */
    public void ShowGameOverOverlay() {
        gameOver.SetActive(true);
    }


    /**
     * Shows the congratulations overlay.
     */
    public void ShowCongratsOverlay() {
        congrats.SetActive(true);
    }
}
