using UnityEngine;

public class Enemy : MonoBehaviour
{
    // set in inspector
    public float speed;

    // private attributes defining different enemy types 
    private string movementType;
    private float health;


    private void Start()
    {

    }


    private void Update()
    {
        //EnemyMovement();
        // temp fix 
        StraightLineMovement(); 
    }


    // basic straight line enemy movement pattern
    private void EnemyMovement()
    {

    }


    private void StraightLineMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }


    // collision checks for player and bullet 
    // called automatically by unity when collision occurs 
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(c.gameObject);
            Score.Instance.HitEnemy();
        }
        else if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
        }
    }


    // kill enemy if it goes off screen 
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("DespawnRange"))
        {
            Destroy(gameObject);
        }
    }
}
