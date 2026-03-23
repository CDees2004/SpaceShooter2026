using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This file is responsible for handling the UI logic on the 
 * StartMenu scene.
 */

public class UIMainMenu : MonoBehaviour
{
    public Button startButtonObject;
    public Button quitButtonObject;


    private void Start()
    {
        // grabbing the button component from the attached UI game object 
        // adding listener to call StartGame method on click 
        if (startButtonObject != null)
        {
            Button startButton = startButtonObject.GetComponent<Button>();
            startButton.onClick.AddListener(StartGame);
        }

        if(quitButtonObject != null)
        {
            Button quitButton = quitButtonObject.GetComponent<Button>();
            quitButton.onClick.AddListener(QuitGame); 
        }
    }


    private void StartGame()
    {
        // loading from main menu to game scene 
        SceneManager.LoadScene("MainGame");
    }


    private void QuitGame()
    {
        // hacky work around to make quitting function 
        // while playing in the editor 
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;

#else
        Application.Quit(); 
#endif

    }

}
