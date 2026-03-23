using UnityEngine;

public class SpaceShooterInput : MonoBehaviour
{
    public static SpaceShooterInput Instance { get; private set; }
    public SpaceShooterInputActions.StandardActions input;


    private void Awake()
    {
        Instance = this;
        var inputActions = new SpaceShooterInputActions();
        inputActions.Enable();
        input = inputActions.Standard;
        input.Enable();
    }


    // disabling and enabling methods for UI to 
    // call to handle UI state appropriately 
    public void DisableInput()
    {
        input.Disable();
    }


    public void EnableInput()
    {
        input.Enable();
    }
}
