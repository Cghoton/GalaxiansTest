using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    [Header("Win Condition")]
    private float Goal = 20f;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private UIController uIController;

    [SerializeField]
    private int EnemiesOnStart = 10;

    private float EnemiesInGame = 0;

    public static GameController Instance;
    

    void Start()
    {
        Instance = this;
        uIController.SetGoal(Goal);

        enemySpawner.InitializeEnemyWallSpawn(EnemiesOnStart);
        enemySpawner.StartCoroutine("ExtraSpawn");
    }

    private void Update()
    {
        if(EnemiesInGame < 3)
            enemySpawner.InitializeEnemyWallSpawn(Random.Range(5,25));

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    
    public void EnemyOut()
    {
        EnemiesInGame--;
    }

    public void EnemyDeath()
    {
        EnemyOut();
        uIController.AddPoints();
    }

    public void EnemySpawned(float amount = 1)
    {
        EnemiesInGame += amount;
    }

    public void EndOfGame()
    {
        GetComponent<SoundController>().Play("GameOver");
        uIController.Lose();
    }

}
