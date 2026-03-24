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

    public static int playerDamage; 

    // private fields
    private static float health;
    private AudioSource audioSource; 
    // make more robust later. Magic numbers are not good. 
    private const float Y_LIMIT = 4.6f;


    private void Start()
    {
        health = 1.0f;
        audioSource = GetComponent<AudioSource>();
        playerDamage = 1; 
    }


    private void Update()
    {
        sliderHealth.value = health;
        // player condition checks and behaviors
        PlayerAttack(); 
        PlayerMovement();
        CheckBounds();

        // check if killed 
        if (health <= 0) SceneManager.LoadScene("GameOver");
    }


    // getter and setter


    // checking player's position against screen bounds 
    // to keep them on screen 
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
        if (this.transform.position.x > screenBoundsRight.bounds.max.x)
        {
            this.transform.position = new Vector3(transform.position.x, screenBoundsRight.bounds.max.x);
        }

        else if (this.transform.position.x < screenBoundsLeft.bounds.min.x)
        {
            this.transform.position = new Vector3(transform.position.x, screenBoundsLeft.bounds.max.x);
        }
    }


    private void PlayerAttack()
    {
        // normal fire 
        if (SpaceShooterInput.Instance.input.Fire.WasPressedThisFrame())
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            // uncomment when normal fire audio clip is added 
            //audioSource.clip = normalFire; 
            //audioSource.Play();
        }
        
        // super fire 
        if (SpaceShooterInput.Instance.input.SuperFire.WasPressedThisFrame())
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bulletObj.GetComponent<Bullet>().speed += 2.0f;
            GameObject bulletObj2 = Instantiate(bulletPrefab, bulletSpawnPoint.position + Vector3.right * 0.5f, Quaternion.identity);
            GameObject bulletObj3 = Instantiate(bulletPrefab, bulletSpawnPoint.position + Vector3.left * 0.5f, Quaternion.identity);
        }
    }


    // Moves the player Verically and Horizontally using Input Action System
    private void PlayerMovement()
    {
        // vertical 
        var vertMove = SpaceShooterInput.Instance.input.MoveVertically.ReadValue<float>();
        this.transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);
        // if the movement is positive enable the particle effect 
      

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

   
    public static void RefillHealth()
    {
        health = 1.0f;
    }


    public static void IncreaseDamage()
    {
        playerDamage++; 
    }
}
