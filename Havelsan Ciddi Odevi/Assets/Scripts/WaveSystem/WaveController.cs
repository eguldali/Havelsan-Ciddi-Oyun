
//This script is used for general level elements of game(difficulty, enemy counter, win condition, enemy waves control)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is used to keep each wave specific values.(Enemy Spawn amount for each levels, Wave end time, total enemySpawns then spawning ended)
public class Wave
{
    int spawnsFrom_Level1;
    int spawnsFrom_Level2;
    int spawnsFrom_Level3; 
    int totalSpawns;
    float givenTime;

    public Wave(int level1Spawns, int level2Spawns, int level3Spawns, float time)
    {
        spawnsFrom_Level1 = level1Spawns;
        spawnsFrom_Level2 = level2Spawns;
        spawnsFrom_Level3 = level3Spawns;
        totalSpawns = spawnsFrom_Level1 + spawnsFrom_Level2 + spawnsFrom_Level3;
        givenTime = time;
    }

    public int SpawnsFrom_Level1
    {
        get
        {
            return spawnsFrom_Level1;
        }
        private set
        {
            spawnsFrom_Level1 = value;
        }
    }
    public int SpawnsFrom_Level2
    {
        get
        {
            return spawnsFrom_Level2;
        }
        private set
        {
            spawnsFrom_Level2 = value;
        }
    }
    public int SpawnsFrom_Level3
    {
        get
        {
            return spawnsFrom_Level3;
        }
        private set
        {
            spawnsFrom_Level3 = value;
        }
    }
    public int TotalSpawns
    {
        get
        {
            return totalSpawns;
        }
        private set
        {
            TotalSpawns = value;
        }
    }
    public float GivenTime
    {
        get
        {
            return givenTime;
        }
        private set
        {
            givenTime = value;
        }
    }



}


public class WaveController : MonoBehaviour
{
    Queue<Wave> WavesQueue = new Queue<Wave>();

    [SerializeField] NonGameplayEventsController nonGameplayEventsController;


    [SerializeField] bool  firstWaveStarted = false;

    float waveTimer = 3f;
    int currentWave = 0;
    int currentEnemyAmount = 0;
    int totalEnemyAmount;
    int killedEnemy;

    [Header("Enemy Spawn Elements")]
    [SerializeField] float minEnemySpawnTime;
    [SerializeField] float maxEnemySpawnTime;
    [SerializeField] SpawnPointLevel[] spawnPointLevels;

    [Header("GUI Elements")]
    [SerializeField] Text text_currWave;
    [SerializeField] Text text_time;
    [SerializeField] Text text_killedEnemy;
    [SerializeField] Text text_Countdown;

    public static WaveController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

  
    // Updates timer for current wave. If timer is 0, it starts next wave. If no wave left timer changes to message.
    void Update()
    {

        if (waveTimer >= 0)
        {
            waveTimer -= Time.deltaTime;

            if(firstWaveStarted == false)
            {
                text_Countdown.text = ((int)waveTimer+1) + "!";
            }
            else
            {
                text_time.text = "Next Wave: " + (int)waveTimer;
            } 
            
        }
        else
        {

            if (WavesQueue.Count != 0)
                StartNextWaveFromQueue();
            else
            {
                text_time.text = "Destroy All!" ;

            }
        }

    }

    // Spawn enemies depends on current Wave values. Waits for a random time between two enemy spawns.
    IEnumerator WaitSpawn(Wave currWave)
    {

        if (currWave.SpawnsFrom_Level1 != 0)
        {
            int i = 0;
            while (i < currWave.SpawnsFrom_Level1)
            {
                spawnPointLevels[0].SpawnRandomEnemyAtRandomPoint();
                float waitTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
                yield return new WaitForSeconds(waitTime);
                i++;
            }
        }
        if (currWave.SpawnsFrom_Level2 != 0)
        {
            int i = 0;
            while (i < currWave.SpawnsFrom_Level2)
            {
                spawnPointLevels[1].SpawnRandomEnemyAtRandomPoint();
                float waitTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
                yield return new WaitForSeconds(waitTime);
                i++;
            }
        }
        if (currWave.SpawnsFrom_Level3 != 0)
        {
            int i = 0;
            while (i < currWave.SpawnsFrom_Level3)
            {
                spawnPointLevels[2].SpawnRandomEnemyAtRandomPoint();
                float waitTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
                yield return new WaitForSeconds(waitTime);
                i++;
            }
        }
    }

    // Starts spawning enemies and updates wave timer and total enemies player should deal with.
    void StartWave(Wave currWave)
    {
        StartCoroutine(WaitSpawn(currWave));
        waveTimer = currWave.GivenTime;
        currentEnemyAmount += currWave.TotalSpawns;

    }

    // Fill the Wave queue with easy difficulty set-up
    void FillEasyQueue()
    {
        Wave wave1 = new Wave(1, 0, 0, 40);
        Wave wave2 = new Wave(1, 1, 1, 70);
        
        WavesQueue.Enqueue(wave1);
        WavesQueue.Enqueue(wave2);

        totalEnemyAmount = wave1.TotalSpawns + wave2.TotalSpawns;
    }

    // Fill the Wave queue with medium difficulty set-up
    void FillMediumQueue()
    {
        Wave wave1 = new Wave(1, 1, 0, 40);
        Wave wave2 = new Wave(1, 2, 0, 70);
        Wave wave3 = new Wave(1, 2, 1, 80);
        
        WavesQueue.Enqueue(wave1);
        WavesQueue.Enqueue(wave2);
        WavesQueue.Enqueue(wave3);

        totalEnemyAmount = wave1.TotalSpawns + wave2.TotalSpawns + wave3.TotalSpawns;
    }

    // Fill the Wave queue with hard difficulty set-up
    void FillHardQueue()
    {
        Wave wave1 = new Wave(1, 1, 0, 40);
        Wave wave2 = new Wave(2, 1, 1, 70);
        Wave wave3 = new Wave(1, 2, 1, 80);
        Wave wave4 = new Wave(1, 2, 1, 80);

        WavesQueue.Enqueue(wave1);
        WavesQueue.Enqueue(wave2);
        WavesQueue.Enqueue(wave3);
        WavesQueue.Enqueue(wave4);

        totalEnemyAmount = wave1.TotalSpawns + wave2.TotalSpawns + wave3.TotalSpawns + wave4.TotalSpawns;
    }

    //Set game difficulty depands on player's selection.
    public void StartQueueByDifficulty(int selectedDifficulty)
    {
        if (selectedDifficulty == 0)
        {
            Debug.Log("Easy Game Started");
            FillEasyQueue();
        }
        else if (selectedDifficulty == 1)
        {
            Debug.Log("Med Game Started");
            FillMediumQueue();
        }
        else if (selectedDifficulty == 2)
        {
            Debug.Log("Hard Game Started");
            FillHardQueue();
        }
    }

    //Starts next wave from Wave queue;
    void StartNextWaveFromQueue()
    {
        StartWave(WavesQueue.Peek());
        WavesQueue.Dequeue();
        currentWave++;
        
        if(firstWaveStarted == false)
        {
            text_Countdown.gameObject.SetActive(false);
            text_currWave.gameObject.SetActive(true);
        }
        firstWaveStarted = true;
        text_currWave.text = "Wave" + currentWave;
    }

    //Called when an enemy destroyed. Checks win condition. Updates killed enemy amount in GUI.
    public void UpdateEnemyCounter()
    {
        killedEnemy++;
        currentEnemyAmount--;
        text_killedEnemy.text = killedEnemy.ToString();

        if (killedEnemy == totalEnemyAmount)
        {
            nonGameplayEventsController.FinishGameWithWin();
        }

    }
}
