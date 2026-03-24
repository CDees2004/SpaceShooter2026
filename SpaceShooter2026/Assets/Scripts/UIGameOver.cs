using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    // buttons of interest 
    public GameObject quitButtonObject;
    public GameObject restartButtonObject;

    public TextMeshProUGUI finalScoreText;

    public GameObject newHighScoreObj;
    public GameObject gameOverObj;

    private static float sessionHighScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // check high score
        float currentScore = Score.Instance.GetScore();

        // display the final score 
        if (finalScoreText != null)
        {
            finalScoreText.text = $"Score: {Score.Instance.GetScore()}";
        }

        // show win screen if user gets new high score 
        if (currentScore > sessionHighScore)
        {
            sessionHighScore = currentScore;
            if (newHighScoreObj != null) newHighScoreObj.SetActive(true);
            if (gameOverObj != null) gameOverObj.SetActive(false);
        }


        else
        {
            if (newHighScoreObj != null) newHighScoreObj.SetActive(false);
            if (gameOverObj != null) gameOverObj.SetActive(true);
        }

        if (restartButtonObject != null)
        {
            Button restartButton = restartButtonObject.GetComponent<Button>();
            restartButton.onClick.AddListener(RestartGame);
        }

        if (quitButtonObject != null)
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
