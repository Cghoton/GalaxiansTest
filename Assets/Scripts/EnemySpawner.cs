using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerTrans;

    [SerializeField]
    private GameObject[] EnemyPrefab;

    [SerializeField]
    [Header("Amount of Enemies in 1 Line")]
    private float EnemiesInLine = 6;

    [SerializeField]
    private Vector3 PositionOffset;

    [SerializeField]
    private float DistanceFromPlayerToSpawner = 16f;

    [SerializeField]
    private float MaxExtraSpawnCooldown = 8f;

    [SerializeField]
    private float MinExtraSpawnCooldown = 3f;


    private Vector3 SpawnPosition;

    private bool GameOn = true;

    public void InitializeEnemyWallSpawn(float EnemiesToSpawn)
    {
        SpawnPosition = transform.position;
        for (int SpawnedEnemy = 0; SpawnedEnemy < EnemiesToSpawn; SpawnedEnemy++)
        {
            EnemySpawn();

            if (SpawnedEnemy % EnemiesInLine == 0)
                SpawnPosition = new Vector3(transform.position.x, transform.position.y, SpawnPosition.z + 2);
            else
                SpawnPosition += PositionOffset;
        }
        GameController.Instance.EnemySpawned(EnemiesToSpawn);
    }

    public IEnumerator ExtraSpawn()
    {
        while (GameOn)
        {
            SpawnExtraEnemy();
            
            yield return new WaitForSeconds(Random.Range(MinExtraSpawnCooldown, MaxExtraSpawnCooldown));
        }
    }

    private void SpawnExtraEnemy()
    {
        SpawnPosition = transform.position + new Vector3(Random.Range(0f, 10), 0, Random.Range(-3f, 3));
        EnemySpawn();
        GameController.Instance.EnemySpawned();
    }

    private void EnemySpawn()
    {
        Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], SpawnPosition, Quaternion.Euler(0,180,0));
    }

    private void Update()
    {
        transform.position = new(transform.position.x, transform.position.y, PlayerTrans.position.z + DistanceFromPlayerToSpawner);
    }
}
