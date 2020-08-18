
//This class is used for spawning enemies in a position.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject enemyXPrefab, enemyYPrefab, enemyZPrefab;

    //Instantiate an enemy type on spawn point. 
    public void SpawnEnemy(int enemyType)
    {
        if(enemyType == 0)
        {
            Instantiate(enemyXPrefab, transform.position, Quaternion.identity);
        }
        else if(enemyType == 1)
        {
            Instantiate(enemyYPrefab, transform.position, Quaternion.identity);

        }
        else if(enemyType == 2)
        {
            Instantiate(enemyZPrefab, transform.position, Quaternion.identity);
        }
    }
}
