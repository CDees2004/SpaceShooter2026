using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    // buttons of interest 
    public GameObject quitButtonObject;
    public GameObject restartButtonObject; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // disabling players input when the game is over 
        

        if(restartButtonObject != null)
        {
            Button restartButton = restartButtonObject.GetComponent<Button>();
            restartButton.onClick.AddListener(RestartGame); 
        }

        if(quitButtonObject != null)
        {
            Button quitButton = quitButtonObject.GetComponent<Button>();
            quitButton.onClick.AddListener(QuitGame); 
        }
    }

 
    // cleans the game state and returns back to the main menu
    private void RestartGame()
    {
        SceneManager.LoadScene("StartMenu"); 
    }

  
    // closes the application 
    private void QuitGame()
    {
#if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
#else
    Application.Quit(); 
#endif
    }
}
