
// This class is used for keep track of spawn points and spawn random enemy in a certain range level.
//Spawn levels are depends on range from enemy. Same spawn points in same level have same distance from player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointLevel : MonoBehaviour
{
    [SerializeField] List<SpawnPoint> spawnPointsList = new List<SpawnPoint>();

    float maxWait, minWait;

    //Spawn random enemy type on a random spawn point that in same spawn level.
    public void SpawnRandomEnemyAtRandomPoint()
    {

        int enemyType = Random.Range(0, 3);
        int spawnPoint = Random.Range(0, spawnPointsList.Count);

        spawnPointsList[spawnPoint].SpawnEnemy(enemyType);

    }



}
