
// This script is used for controlling Fire System of Player's gun

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponFireSystem : MonoBehaviour
{
    Gun myGun;
    private float energyLoadingDuration = 0f;
    private bool loadingEnergy = false;

    [SerializeField] Transform bulletFireTransform;

    public GameObject bulletEnergyShower;

    float loadedEnergy;
    

    [Header("GUI Elements")]
    [SerializeField] Text text_currMag;

    [Header("Audio")]
    [SerializeField] AudioSource shipAudioSource;
    public AudioClip shootSound, fillSound;


    

    void Awake()
    {
        myGun = GetComponent<Gun>();
       
    }

    //Sets current magazine of Player's gun, at the start of the game.
    void Start()
    {

        text_currMag.text = myGun.battaryMagazine.ToString();
       
    }

    // Gets Input for energy loading if gun can shoot. After energy loading done, calls Fire function.
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            if (myGun.canFire == true)
            {
                loadingEnergy = true;
            }
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            if (energyLoadingDuration != 0f)
            {
                Fire();

                loadingEnergy = false;
            }
        }

    }

    //Updates current magazine, loading energy duration, bulletEnergyShower scale that depends on loaded energy values. 
    void LateUpdate()
    {
        text_currMag.text = myGun.battaryMagazine.ToString();

        if (loadingEnergy == true)
        {
            if (energyLoadingDuration < 5.0f)
            { 
                energyLoadingDuration += Time.deltaTime;
                bulletEnergyShower.SetActive(true);
                bulletEnergyShower.transform.localScale += new Vector3(Time.deltaTime * 0.15f, Time.deltaTime * 0.15f, Time.deltaTime * 0.15f);
                if(shipAudioSource.isPlaying == false)
                {
                    shipAudioSource.clip = fillSound;
                    shipAudioSource.Play();
                }
            }
            else if(energyLoadingDuration >= 5.0f)
            {
                energyLoadingDuration = 5.0f;
            }
        }
        else
        {
            bulletEnergyShower.SetActive(false);
            bulletEnergyShower.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            energyLoadingDuration = 0f;
            if (myGun.battaryMagazine < myGun.MaxBattery)
            {
                myGun.battaryMagazine += (int)(Time.deltaTime * 70);
            }
            else
            {
                myGun.battaryMagazine = myGun.MaxBattery;
            }
        }
    }

    //Calculates loaded energy value for bullet and call FireBullet function.
    void Fire()
    {
       
        loadedEnergy =(float)(Math.Pow(2, energyLoadingDuration)) * 100f;
        if (myGun.battaryMagazine >= (int)loadedEnergy)
            myGun.battaryMagazine -= (int)loadedEnergy;
        else
            loadedEnergy = myGun.battaryMagazine;
        
        FireBullet();
    }

    // Activate a bullet from bullet pool and assign its parameters.(firstposition, rotation, scale that depends on loaded energy, life time depends on gun type. Also Updates fire amount game info.
    void FireBullet()
    {
        Bullet bulletClone = BulletPool.Instance.GetBulletFromQueue();
        bulletClone.transform.position = bulletFireTransform.position;
        bulletClone.transform.forward = transform.parent.forward;
        bulletClone.transform.localScale = bulletEnergyShower.transform.localScale;
        bulletClone.myLoadedEnergy = loadedEnergy;
        bulletClone.lifeTime = myGun.myBulletLifetime;
        bulletClone.gameObject.SetActive(true);
        bulletClone.FireBullet();
        GameInfoKeeper.Instance.UpdateFireAmount();

        shipAudioSource.clip = shootSound;
        shipAudioSource.Play();
        

    }
}
