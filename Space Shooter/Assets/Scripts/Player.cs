using UnityEngine;

public class Player : MonoBehaviour
{
    private SpaceShooterInputs inputActions;

    // public fields with certain primitive types allow the type 
    // to appear in the inspector allowing on the fly modification
    public float speed = 0.03f;

    private const float Y_LIMIT = 4.6f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new();
        // need to enable input actions manually
        inputActions.Enable();
        inputActions.Standard.Enable(); 
    }

    // Update is called once per frame
    void Update()
    {
        // reading if the user hit the up key 
        if (inputActions.Standard.MoveUp.IsPressed())
        {
            // this refers to the object that the script is 
            // attached to in the editor 
            //this.transform.Translate(new Vector3(0, 0.1f, 0)); 
           
            // different approach 
            this.transform.Translate(Vector3.up * speed); 
        }

        // reading if otherwise the user hit the down button 
        else if (inputActions.Standard.MoveDown.IsPressed())
        {
            this.transform.Translate(Vector3.down * speed); 
        }


        // clamping the player's position to keep them within screen bounds 
        // value is currently hard coded 
        if(this.transform.position.y > Y_LIMIT)
        {
            this.transform.position = new Vector3(0.0f, Y_LIMIT, 0.0f);
        }

        if (this.transform.position.y < -Y_LIMIT)
        {
            this.transform.position = new Vector3(0.0f, -Y_LIMIT, 0.0f);

        }

    }
}
