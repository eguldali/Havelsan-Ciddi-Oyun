
//This script is used for load player informations saved from gameplay while in FinalScene. 


using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoLoader : MonoBehaviour
{
    int fireAmount;
    int hitAmount;
    float gunAccuracy;
    float totalDamage;
    int killedEnemy;
    List<float> enemyKillRangeList = new List<float>();

    string gameTime;

    [SerializeField] Text[] endingText;

    //Load the saved data before showing it.
    void Awake()
    {
        LoadGame();
    }
  
    // Show the saved data in the start of the scene
    void Start()
    {
        ShowResult();
    }

    //Find the save document and deserialize it. 
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.txt"))
        {
           
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.txt", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();


            fireAmount = save.fireAmount;
            hitAmount = save.hitAmount;
            gunAccuracy = save.gunAccuracy;
            totalDamage = save.totalDamage;
            killedEnemy = save.killedEnemy;
            gameTime = save.gameTime;


            if (save.enemyKillRangeList.Count != 0)
            {
                foreach (float range in save.enemyKillRangeList)
                {
                    enemyKillRangeList.Add(range);
                }
            }
        }
    }

    //Use deserialized from LoadGame and use it for GUI texts. 
    void ShowResult()
    {
        endingText[0].text = "Total Fire: " + fireAmount;
        endingText[1].text = "Total Hit: " + hitAmount;
        endingText[2].text = "Gun Accuracy: " + gunAccuracy + "%";
        endingText[3].text = "Total Damage: " + totalDamage;
        endingText[4].text = "Total Desteroyed Enemy: " + killedEnemy;
        endingText[5].text = "Game Time: " + gameTime;
        

    }
    

}
