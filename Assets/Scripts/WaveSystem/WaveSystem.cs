using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public static WaveSystem instance;

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int[] enemySpawnsEachRounds;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemySpawnTimeIntervals = 0.2f;

    protected int currentSpawnPointIndex = 0;
    protected int currentWaveIndex = 0;
    protected int overAllWaveIndex = 0;
    protected int currentEnemyCountInCurrentWave = int.MaxValue;
    protected bool coroutineRunning = false;
    protected float increaseSpeedPerWave = 1f;


    [SerializeField] protected int currentNumberOfEnemiesOnScreen = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 unit action system  " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    private void Start()
    {
        currentEnemyCountInCurrentWave = enemySpawnsEachRounds[0];
    }

    private void Update()
    {

        if (!coroutineRunning && currentNumberOfEnemiesOnScreen <= 0)
        {
            StartCoroutine(spawnEnemyInCurrentWave());
        }
    }


    /// <summary>
    /// This coroutine will start when there are no enemies left on screen
    /// </summary>
    /// <returns></returns>

    IEnumerator spawnEnemyInCurrentWave()
    {
        coroutineRunning = true;
        int enemiesToSpawn = enemySpawnsEachRounds[currentWaveIndex];
        ScoreSystem.instance.displayWaveStartNum(overAllWaveIndex);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            yield return new WaitForSecondsRealtime(enemySpawnTimeIntervals);
            Debug.Log("Instantiating enemy");
            GameObject gameObject = Instantiate(enemyPrefab.gameObject, spawnPoints[currentSpawnPointIndex++ % spawnPoints.Length].position, Quaternion.identity);
            currentNumberOfEnemiesOnScreen++;
            gameObject.GetComponent<EnemyMovement>().afterWave6IncreaseEnemySpeed(increaseSpeedPerWave);

        }
        IncreasePerWaveMoveSpeedIncreement();
        coroutineRunning = false;
        currentWaveIndex++;
        overAllWaveIndex++;
        if (currentWaveIndex == enemySpawnsEachRounds.Length)
        {
            currentWaveIndex = enemySpawnsEachRounds.Length - 1;
        }
    }

    private void IncreasePerWaveMoveSpeedIncreement()
    {
        if (increaseSpeedPerWave <= 10f)
        {
            increaseSpeedPerWave += 3f;
        }
    }

    public void reduceCurrentNumberOfEnemiesOnScreen()
    {
        currentNumberOfEnemiesOnScreen--;
    }

    
}
