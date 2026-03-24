using UnityEngine;

public class Enemy : MonoBehaviour
{
    // set in inspector
    public float speed;
    // private attributes defining different enemy types 
    private string movementType;
    // doing ints as health to keep everything very simple
    public int health;

    // only large enemies track 
    public bool isTrackingEnemy = false;
    private Transform playerTransform;

    public GameObject explosionPrefab;



    private void Start()
    {
        // finding the player object 
        if (isTrackingEnemy)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
    }


    private void Update()
    {
        if (isTrackingEnemy)
        {
            EnemyTrackingMovement();
        }
        else
        {
            StraightLineMovement();
        }
    }


    private void Explode()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
        }
    }


    public void InitEnemyHealth(int round)
    {
        // give enemies +1 health every 2 rounds 
        health = 1 + (round - 1) / 2;
        print("Health is" + health);
    }


    // basic straight line enemy movement pattern
    private void EnemyTrackingMovement()
    {
        if (playerTransform == null) return;

        // get dir to move toward player 
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // move toward that dir 
        transform.Translate(direction * speed * Time.deltaTime);
    }


    private void StraightLineMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }


    private int DamageToEnemy(int playerDamage)
    {
        // enemies health is decreased in response to damage
        health -= playerDamage;
        // returns health remaining after attack 
        return health;
    }


    // collision checks for player and bullet 
    // called automatically by unity when collision occurs 
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            DamageToEnemy(Player.playerDamage);
            Destroy(c.gameObject);
            // only destroy the enemy if the player damages 
            // them for more than their health 
            if (health <= 0)
            {
                Explode(); 
                Destroy(gameObject);
                Score.Instance.HitEnemy();
                Game.Instance.EnemyDestroyed();
            }
        }

        else if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
            Game.Instance.EnemyDestroyed();
        }
    }


    // kill enemy if it goes off screen 
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("DespawnRange"))
        {
            Destroy(gameObject);
            Game.Instance.EnemyDestroyed();
        }
    }
}
