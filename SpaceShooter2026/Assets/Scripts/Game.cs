using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    // making game a singleton so enemy can tell us when an enemy has died 
    public static Game Instance { get; private set; }

    // set in inspector
    public float enemySpawnDelay;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public BoxCollider2D spawnRange;

    public float roundTextDuration = 2.0f;

    // private fields
    private float powerUpDelay;
    private float powerupSpawnTimer;

    public TextMeshProUGUI txtRound;

    // enemy spawning fields 
    private float enemySpawnTimer;
    private int currentSpawnRound;
    private int defeatedEnemies;
    private int currentSpawnedEnemies;
    private int enemySpawnCount = 10; // amount per round. This amount is multiplied by the round number


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        // initializing round 1 values and performing needed initial calls 
        powerUpDelay = Random.Range(5f, 10f);
        powerupSpawnTimer = 0;

        // intializing the game round 
        currentSpawnRound = 1;
        defeatedEnemies = 0;
        currentSpawnedEnemies = 0;

        // this might not be neccesary 
        if (txtRound != null)
        {
            txtRound.gameObject.SetActive(false);
        }

        // starting a coroutine that is where our game loop is stored 
        StartCoroutine(RoundLoop());

    }


    private void Update()
    {
        // check spawn powerup
        powerupSpawnTimer += Time.deltaTime;
        if (powerupSpawnTimer >= powerUpDelay)
        {
            SpawnPowerup();
            powerUpDelay = Random.Range(5, 10);
            powerupSpawnTimer = 0.0f;
        }
    }


    // infinite loop of rounds spawning more and more enemies 
    // going for high score based gameplay loop 
    private IEnumerator RoundLoop()
    {
        // rounds continue endlessly 
        while (true)
        {
            // take detour to show popup before continuing round 
            yield return StartCoroutine(RoundPopup());

            // reset values for new round start 
            defeatedEnemies = 0;
            currentSpawnedEnemies = 0;
            // spawning enemies 
            StartCoroutine(SpawnEnemiesRoutine());

            // waiting until all the enemies are dead to progress 
            // NOTE: need to make the check a lambda func because it requires a func as param 
            // waits until defeated enemies amount is equal to the amount spawned overall
            int totalEnemySpawn = enemySpawnCount * currentSpawnRound; 
            yield return new WaitUntil(() => defeatedEnemies >= totalEnemySpawn);

            // increment round 
            currentSpawnRound++;

            // shop popup after round concludes 
            //yield return StartCoroutine(ShopPopup()); 
        }
    }


    // 
    private IEnumerator SpawnEnemiesRoutine()
    {
        // spawning enemy amount determined by the round number 
        for (int i = 0; i < enemySpawnCount * currentSpawnRound; i++)
        {
            //enemySpawnTimer += Time.deltaTime;
            //if (enemySpawnTimer >= enemySpawnDelay)
            //{
            SpawnEnemy();
            //enemySpawnTimer = Random.Range(0, 5);
            // waiting on an enemy spawn to complete before iterating 
            yield return new WaitForSeconds(Random.Range(1, 5));
            //}
        }
    }


    // creates an enemy within the enemy spawn range 
    private void SpawnEnemy()
    {
        Vector3 enemySpawnPt = new Vector3(
            Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
            Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),
            0);
        Instantiate(enemyPrefab, enemySpawnPt, Quaternion.identity);
        // updating the spawned enemy amount 
        currentSpawnedEnemies++;
    }



    // the coroutine for the round UI panel pop up 
    private IEnumerator RoundPopup()
    {
        if (txtRound != null)
        {
            // set the text to display the round
            txtRound.text = $"Round {currentSpawnRound}";
            txtRound.gameObject.SetActive(true);
            // stopping our program to show the round for 2 seconds 
            yield return new WaitForSeconds(2.0f);
            txtRound.gameObject.SetActive(false);

        }
    }


    // method called by enemy through singleton to let us know enemy died 
    public void EnemyDestroyed()
    {
        defeatedEnemies++;
    }


    // spawns shield regen at random intervals
    // spawned from same location as enemies
    private void SpawnPowerup()
    {
        Vector3 powerupSpawnPt = new Vector3(
            Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
            Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),
            0);
        Instantiate(powerupPrefab, powerupSpawnPt, Quaternion.identity);
    }
}
