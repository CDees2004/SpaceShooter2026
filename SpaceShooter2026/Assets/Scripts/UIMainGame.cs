using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainGame : MonoBehaviour
{
    // these elements will be panels, not buttons 
    public GameObject pauseUIElement;
    public GameObject shopUIElement;

    private static bool gamePaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // if the user hits the escape key then enable the 
        // pause panel and stop the game 
        if (SpaceShooterInput.Instance.input.Pause.WasPressedThisFrame())
        {
            PauseGameToggle();
        }
    }


    // pause the game by freezing the time 
    // and activating the UI element 

    private void PauseGameToggle()
    {
        print("PauseGameToggle called");
        // only work if we are playing the game 
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            // invert the current game state 
            pauseUIElement.SetActive(!(gamePaused));
            // invert paused state
            gamePaused = !(gamePaused);
            // invert time scale 
            if (gamePaused)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

}
