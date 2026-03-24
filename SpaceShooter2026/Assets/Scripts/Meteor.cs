using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed; 

    // Update is called once per frame
    private void Update()
    {
        Movement(); 
    }

    private void Movement()
    {
        transform.Translate((Vector3.left + Vector3.down).normalized * speed * Time.deltaTime);
    }

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
