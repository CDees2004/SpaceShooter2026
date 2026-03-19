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
            c.gameObject.GetComponent<Player>().RefillShield();
        }
    }
}
