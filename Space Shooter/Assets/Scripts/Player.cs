using UnityEngine;

public class Player : MonoBehaviour
{
    // public fields with certain primitive types allow the type 
    // to appear in the inspector allowing on the fly modification
    public float speed = 0.03f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; 

    private SpaceShooterInputs inputActions;

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
        if (inputActions.Standard.Fire.WasPressedThisFrame())
        {
            // instantiating the prefab 
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity); 
        }


        // reading if the user hit the up key 
        if (inputActions.Standard.MoveUp.IsPressed())
        {
            // this refers to the object that the script is 
            // attached to in the editor 
            //this.transform.Translate(new Vector3(0, 0.1f, 0)); 
           
            // different approach 
            // times delta time to make speed independent from frame rate 
            this.transform.Translate(Vector3.up * speed * Time.deltaTime); 
        }

        // reading if otherwise the user hit the down button 
        else if (inputActions.Standard.MoveDown.IsPressed())
        {
            this.transform.Translate(Vector3.down * speed * Time.deltaTime); 
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
