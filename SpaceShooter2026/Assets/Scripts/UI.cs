using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class UI : MonoBehaviour
{
    public Button startButtonObject; 

    private void Start()
    {
        // grabbing the button component from the attached UI game object 
        // adding listener to call StartGame method on click 
        Button startButton = startButtonObject.GetComponent<Button>();
        startButton.onClick.AddListener(StartGame); 
    }


    private void Update()
    {

    }

    private void StartGame()
    {
        // loading from main menu to game scene 
        SceneManager.LoadScene("MainGame"); 
    }

}
