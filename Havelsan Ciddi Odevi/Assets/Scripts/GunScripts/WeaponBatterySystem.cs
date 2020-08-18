
// This script is used for controlling Battery System of Player's gun

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBatterySystem : MonoBehaviour
{
    Gun myGun;
    private int batterySystemType;

    // Start is called before the first frame update
    void Awake()
    {
        myGun = GetComponent<Gun>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //Sets the canon type of gun by its code.
    public void SelectBatterySystem(int typeCode)
    {
        batterySystemType = typeCode;
        if (typeCode == 0)
        {
            myGun.battaryMagazine = 5000;
            myGun.myBulletLifetime = 1f;
        }
        else if (typeCode == 1)
        {
            myGun.battaryMagazine = 10000;
            myGun.myBulletLifetime = 1.5f;
        }
        else if(typeCode == 2)
        {
            myGun.battaryMagazine = 15000;
            myGun.myBulletLifetime = 2f;
        }
    }



}
