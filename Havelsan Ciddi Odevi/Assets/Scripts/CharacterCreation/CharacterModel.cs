
// This class is used to access and set the Canon & LRF models for both in StartScene & GameScene 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{

    [SerializeField] GameObject[] canons;
    [SerializeField] GameObject[] lrfs;

    // Getter, Setter for Characters Canon Models 
    public GameObject[] Canons
    {
        get
        {
            return canons;
        }
        private set
        {
            Canons = value;
        }
    }

    // Getter, Setter for Characters LRF Models
    public GameObject[] Lrfs
    {
        get
        {
            return lrfs;
        }
        private set
        {
            lrfs = value;
        }
    }

    // Select a Canon with Buttons
    public void SetSelectedCanon(int selectedCanon)
    {
        PlayerPrefs.SetInt("Canon", selectedCanon);
        foreach (GameObject obj in canons)
        {
            obj.SetActive(false);
        }
        canons[PlayerPrefs.GetInt("Canon")].SetActive(true);
    }
    
    // Select a LRF with Buttons
    public void SetSelectedLRF(int selectedLrf)
    {
        PlayerPrefs.SetInt("LRF", selectedLrf);
        foreach (GameObject obj in lrfs)
        {
            obj.SetActive(false);
        }
        lrfs[PlayerPrefs.GetInt("LRF")].SetActive(true);

    }

    //Update character Canon & LRF models by PlayerPrefs data
    public void ShowSelectedCharacter()
    {
        foreach (GameObject obj in canons)
        {
            obj.SetActive(false);
        }

        canons[PlayerPrefs.GetInt("Canon")].SetActive(true);

        foreach (GameObject obj in lrfs)
        {
            obj.SetActive(false);
        }

        lrfs[PlayerPrefs.GetInt("LRF")].SetActive(true);

    }


}
