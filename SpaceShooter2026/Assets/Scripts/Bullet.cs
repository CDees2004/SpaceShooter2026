using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 95f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the bullet crosses the enemy spawn then destroy it
        if (collision.gameObject.CompareTag("EnemySpawnRange"))
        {
            Destroy(gameObject);
        }
    }
}
