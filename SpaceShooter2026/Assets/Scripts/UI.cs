using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button startButtonObject;
    public GameObject PausePanel;

    private void Start()
    {
        // grabbing the button component from the attached UI game object 
        // adding listener to call StartGame method on click 
        if (startButtonObject != null)
        {
            Button startButton = startButtonObject.GetComponent<Button>();
            startButton.onClick.AddListener(StartGame);
        }
    }


    private void Update()
    {
        if (SpaceShooterInput.Instance.input.Pause.WasPressedThisFrame() && SceneManager.GetActiveScene().name == "MainGame")
        {
            // pausing the game 
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    private void StartGame()
    {
        // loading from main menu to game scene 
        SceneManager.LoadScene("MainGame");
    }

}
