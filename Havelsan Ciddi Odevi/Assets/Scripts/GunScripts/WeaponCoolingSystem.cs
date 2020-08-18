
// This script is used for controlling Cooling System of Player's gun

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCoolingSystem : MonoBehaviour
{
    Gun myGun;
    bool loadingEnergy;
    int minTemp = 20;
    int maxTemp = 70;
    float currentTemp;
    float cooldownDuration;
    int tempIncreseSpeed = 10;
    float tempDecrSpeed;

    [Header("GUI Elements")]
    [SerializeField] Text text_currTemp;
    // Start is called before the first frame update

    void Awake()
    {
        myGun = GetComponent<Gun>();
    }

    //Sets current Temp to min Temp when gameplay started.
    void Start()
    {
        currentTemp = minTemp;

    }

    // Gets Input for energy loading if gun can shoot. After enrgy loading done, calculate cooldown duration.
    void Update()
    {
        if (myGun.canFire == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                loadingEnergy = true;

            }
            else if (Input.GetButtonUp("Fire1"))
            {

                CalculateCooldownDuration();
                loadingEnergy = false;

            }
        }
    }

    // Calculates current gun temperature by looking energy loading and decide if gun is overheated. Updates temperature text in GUI.
    void LateUpdate()
    {
        text_currTemp.text = ((int)currentTemp).ToString();
        if (loadingEnergy == true)
        {

            if (currentTemp < 70)
                currentTemp += (Time.deltaTime * tempIncreseSpeed);
            else
                currentTemp = 70;

        }
        else if(loadingEnergy == false)
        {

            if (cooldownDuration > 0)
            {
                
                cooldownDuration -= Time.deltaTime;
                currentTemp -= tempDecrSpeed * Time.deltaTime;
                myGun.overheated = true;
            }
            else
            {
                currentTemp = 20;
                cooldownDuration = 0;
                myGun.overheated = false;
            }
        }
        

    }
    
    //Calculates Cooldown duration depends on current gun temperature. 
    void CalculateCooldownDuration()
    {
        
       cooldownDuration = (float)Math.Pow(2, (currentTemp / 10)) / (float)Math.Pow(2, (currentTemp / 20));
       tempDecrSpeed = ((currentTemp - 20) / cooldownDuration);

    }
   
}
