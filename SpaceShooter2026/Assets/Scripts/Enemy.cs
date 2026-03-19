using UnityEngine;

public class Enemy : MonoBehaviour
{
    // set in inspector
    public float speed;

    void Update()
    {
        EnemyMovement();
    }

    // basic straight line enemy movement pattern
    private void EnemyMovement()
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
