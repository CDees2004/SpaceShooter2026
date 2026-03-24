using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 95f;

    public GameObject initialBullet;
    public GameObject boostedBullet;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        SpriteSwap(); 
    }


    // Update is called once per frame
    private void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void SpriteSwap()
    {
        if (Player.damageIncreased)
        {
            initialBullet.SetActive(false);
            boostedBullet.SetActive(true); 
        }
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
