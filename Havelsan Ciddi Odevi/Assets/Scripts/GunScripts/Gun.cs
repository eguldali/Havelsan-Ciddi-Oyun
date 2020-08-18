using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int battaryMagazine;
    public int MaxBattery;
    public float myBulletLifetime;

    WeaponBatterySystem weaponBatterySystem;
    WeaponFireSystem weaponFireSystem;
    WeaponCoolingSystem weaponCoolinSystem;

    [Header("Gun Controllers")]
    public bool canFire = true;
    public bool overheated = false;
    public bool inLrfMode = false;
    public bool gamePaused = false;

    [Header("GUI Elements")]
    [SerializeField] Text text_maxMagazine;
    [SerializeField] Text text_curMagazine;
    [SerializeField] Image magazineImage;


    //Sets the selected gun parameters, during StartScene, when GameScene started.
    void Start()
    {
        weaponBatterySystem = GetComponent<WeaponBatterySystem>();
        weaponFireSystem = GetComponent<WeaponFireSystem>();
        weaponCoolinSystem = GetComponent<WeaponCoolingSystem>();

        weaponBatterySystem.SelectBatterySystem(PlayerPrefs.GetInt("Canon"));
        text_maxMagazine.text = "/ " + battaryMagazine; 
        text_curMagazine.text = battaryMagazine.ToString();
        MaxBattery = battaryMagazine;
    }


    //Looking for if gun can shoot or not. Updates magazine image depends on battery usage.
   void Update()
    {
        if (overheated == false && inLrfMode == false && gamePaused == false)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }

        float magazinePerc = battaryMagazine * 100 / MaxBattery;
        magazineImage.fillAmount = magazinePerc / 100;    
    }




}
