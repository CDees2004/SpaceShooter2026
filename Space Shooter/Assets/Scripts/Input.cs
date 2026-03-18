using UnityEngine;

public class SpaceShooterInput : MonoBehaviour {
  public static SpaceShooterInput Instance { get; private set; }
  public SpaceShooterInputs.StandardActions input;

  private void Awake() {
    Instance = this;
    var inputActions = new SpaceShooterInputs();
    inputActions.Enable();
    input = inputActions.Standard;
    input.Enable();
  }
}
