
// Derived class of Enemy as ZEnemy.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZEnemy : Enemy
{
    float X_health = 2500f;
    float X_moveSpeed = 0.1f;

    [Header("Child Class Elements")]
    [SerializeField] GameObject[] ObjectElements;
    [SerializeField] GameObject radarObject;

  
    void Start()
    {
        FillEnemyParameters();
    }

  
    void Update()
    {
        base.MoveTowardPlayer();
    }

    //Set base classes parameters with type values
    public override void FillEnemyParameters()
    {
        base.health = X_health;
        base.moveSpeed = X_moveSpeed;
        base.fullHealth = base.health;
    }
    
    //Change layer of objects, that creates model of enemy, with SeenToEye layer 
    public override void ShowYourSelfToCamera()
    {
        foreach (GameObject obj in ObjectElements)
        {
            obj.layer = 10;
        }
    }

    //Change layer of radar object,that only seen by LRF Camera as a dot, with SeenToRadar layer 
    public override void ShowYourSelfToRadar()
    {
        radarObject.layer = 11;
    }

    //Change layer of radar object, HiddenToRadar layer 
    public override void HideYourSelFromRadar()
    {

        radarObject.layer = 9;

    }


}
