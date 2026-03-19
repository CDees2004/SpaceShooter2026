using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    // set in inspector
    public float speed = 0.1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Slider sliderHealth;
    public Shield shield;
    public BoxCollider2D screenBoundsLeft; 
    public BoxCollider2D screenBoundsRight; 

    // private fields
    private float health;
    // make more robust later. Magic numbers are not good. 
    private const float Y_LIMIT = 4.6f;

    private void Start()
    {
        health = 1.0f;
    }

    private void Update()
    {
        sliderHealth.value = health;

        // player attack
        if (SpaceShooterInput.Instance.input.Fire.WasPressedThisFrame())
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }

        PlayerMovement();
        CheckBounds();
        // check if killed 
        if (health <= 0) SceneManager.LoadScene("GameOver");

        
    }

    public void CheckBounds()
    {
        // keeping player on screen verically 
        if (this.transform.position.y > Y_LIMIT)
        {
            this.transform.position = new Vector3(transform.position.x, Y_LIMIT);
        }
        else if (this.transform.position.y < -Y_LIMIT)
        {
            this.transform.position = new Vector3(transform.position.x, -Y_LIMIT);
        }

        // keeping player on screen horizontally 
        if(this.transform.position.x > screenBoundsRight.bounds.max.x)
        {
            this.transform.position = new Vector3(transform.position.x, screenBoundsRight.bounds.max.x);
        }

        else if (this.transform.position.x < screenBoundsLeft.bounds.min.x)
        {
            this.transform.position = new Vector3(transform.position.x, screenBoundsLeft.bounds.max.x);
        }
    }

    // Moves the player Verically and Horizontally using Input Action System
    public void PlayerMovement()
    {
        // vertical 
        var vertMove = SpaceShooterInput.Instance.input.MoveVertically.ReadValue<float>();
        this.transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);

        // horizontal 
        var horiMove = SpaceShooterInput.Instance.input.MoveHorizontally.ReadValue<float>();
        this.transform.Translate(Vector3.right * speed * Time.deltaTime * horiMove);
    }


    // Reduces players health if shield is inactive
    public void DamageFromEnemy()
    {
        if (!shield.IsActive)
        {
            health -= 0.25f;
        }
    }


    // Fills shield to max 
    public void RefillShield()
    {
        shield.FullRefill();
    }
}
