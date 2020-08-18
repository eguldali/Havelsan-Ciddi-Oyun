
//This script is used for save player informations during gameplay while passing to FinalScene. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


//Save class to keep wanted variables during gameplay.
[System.Serializable]
public class Save
{
    public int fireAmount;
    public int hitAmount;
    public float gunAccuracy;
    public float totalDamage;
    public int killedEnemy;
    public string gameTime;
    public List<float> enemyKillRangeList = new List<float>();

}


public class GameInfoKeeper : MonoBehaviour
{
    int fireAmount;
    int hitAmount;
    float gunAccuracy;
    float totalDamage;
    int killedEnemy;
    List<float> enemyKillRangeList = new List<float>();

    string gameTime;


    public static GameInfoKeeper Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Adds +1 to hit amount.
    public void UpdateHitAmount()
    {
        hitAmount++;
        Debug.Log("Accuracy: " + CalculateAccuracy());

    }

    // Adds +1 to fire amount.
    public void UpdateFireAmount()
    {
        fireAmount++;

    }

    // Adds +1 to killed enemies.
    public void UpdateKilledEnemy()
    {
        killedEnemy++;
    }
    
    // Adds damage to total player damage has done to enemies.
    public void UpdateTotalDamage(float damage)
    {
        totalDamage += damage;
    }

    // Adds range of player to destroyed enemy to enemyKillRangeList.
    public void UpdateRangeList(float killRange)
    {
        enemyKillRangeList.Add(killRange);
    }

    //Sets End time when game finished.
    public void SetEndTime(float _gameTime)
    {
        Debug.Log("Geldi:" + _gameTime);
        
        int minutes = Mathf.FloorToInt(_gameTime / 60F);
        int seconds = Mathf.FloorToInt(_gameTime - minutes * 60);
        gameTime = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    //Set player shooting accuracy depends on hit amount and fire amount.
    float CalculateAccuracy()
    {

        float finalAccuracy;
        if (fireAmount != 0)
        {
            finalAccuracy = hitAmount * 100 / fireAmount;
            return finalAccuracy;
        }
        else
        {
            return 0f;
        }
    }

    //Create a Save object depends on recorded informations.
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        foreach (float range in enemyKillRangeList)
        {
            save.enemyKillRangeList.Add(range);
        }

        save.hitAmount = hitAmount;
        save.killedEnemy = killedEnemy;
        save.fireAmount = fireAmount;
        save.gunAccuracy = CalculateAccuracy();
        save.totalDamage = totalDamage;
        save.gameTime = gameTime;
       
        return save;
    }

    //Save a Save object as a file named "gamesave.txt" for loading in FinalScene and Load FinalScene.
    public void SaveEndExitGame()
    {
        Time.timeScale = 1f;

        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.txt");
        bf.Serialize(file, save);
        file.Close();

        SceneManager.LoadScene(2);
    }


}
