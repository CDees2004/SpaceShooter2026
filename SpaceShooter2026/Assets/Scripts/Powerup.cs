using UnityEngine;

public class Powerup : MonoBehaviour
{
    // set in inspector
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        // currently if the player shoots the powerup it destroys it 
        if (c.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(c.gameObject);
        }
        else if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(c.gameObject);
            c.gameObject.GetComponent<Player>().RefillShield();
        }
    }


    // destroying the powerup if it goes off screen 
    // note: the style I am using to implement this is different than the 
    // above logic which was used in the demo. I am just handling this the 
    // way which makes most sense to me 
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("DespawnRange"))
        {
            Destroy(gameObject);
            //Destroy(c.gameObject);
        }
    }
}
