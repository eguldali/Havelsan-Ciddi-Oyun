
//This script is used to set Players gameplay selections(model, name, difficulty, canon&lrf type).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterSelection : MonoBehaviour
{

    [SerializeField] GameObject[] displayableCharacters;

    [SerializeField] InputField nameInputField;

    CharacterModel myPlayer;

    int currDifficulty;
    bool soundMuted = false;

    [Header("GUI Elements")]
    [SerializeField] Button[] difficultyButtons;
    [SerializeField] GameObject[] soundButtonImgs;
    [SerializeField] Text[] CharacterInfoTexts;
    [SerializeField] Text[] CanonInfoTexts;
    [SerializeField] Text[] LRFInfoTexts;

    [Header("Animations")]
    [SerializeField] Animator canvasAnimator;

    [Header("Materials")]
    public Material model1Material;
    public Material model2Material;
    public Material canonMaterial;  
    public Material lrfMaterial;

    [Header("Audio")]
    [SerializeField] AudioSource buttonSound;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource canvasSound;

    //Starts the scene according to last changes saved.
    void Start()
    {
        myPlayer = displayableCharacters[PlayerPrefs.GetInt("Character")].GetComponent<CharacterModel>();
        ShowSelectedCharacter();
        ShowInfoTextsOnStart();
        SelectDifficulty(PlayerPrefs.GetInt("selectedDifficulty"));
        nameInputField.text = PlayerPrefs.GetString("playerName");

        if (PlayerPrefs.GetInt("SoundOpt") == 1)
            ChangeSoundOption();
    }

    // Set the character models according to selections of player.
    void ShowSelectedCharacter()
    {
        foreach(GameObject obj in displayableCharacters)
        {
            obj.SetActive(false);
        }
        foreach (Text infoText in CharacterInfoTexts)
        {
            infoText.gameObject.SetActive(false);
        }
        CharacterInfoTexts[PlayerPrefs.GetInt("Character")].gameObject.SetActive(true);
        displayableCharacters[PlayerPrefs.GetInt("Character")].SetActive(true);
        myPlayer = displayableCharacters[PlayerPrefs.GetInt("Character")].GetComponent<CharacterModel>();
        myPlayer.ShowSelectedCharacter();
    }

    // Set the info texts according to selections of player.
    void ShowInfoTextsOnStart()
    {
        CharacterInfoTexts[PlayerPrefs.GetInt("Character")].gameObject.SetActive(true);
        CanonInfoTexts[PlayerPrefs.GetInt("Canon")].gameObject.SetActive(true);
        LRFInfoTexts[PlayerPrefs.GetInt("LRF")].gameObject.SetActive(true);
    }

    //Button action for Character Selection. 
    public void SetSelectedCharacter(int selectedCharacter)
    {
        PlayerPrefs.SetInt("Character", selectedCharacter);
        ShowSelectedCharacter();
    }

    //Button action for Canon Selection. 
    public void SetSelectedCanon(int selectedCanon)
    {
        myPlayer.SetSelectedCanon(selectedCanon);

        foreach (Text infoText in CanonInfoTexts)
        {
            infoText.gameObject.SetActive(false);
        }
        CanonInfoTexts[selectedCanon].gameObject.SetActive(true);
    }

    //Button action for LRF Selection.
    public void SetSelectedLRF(int selectedLRF)
    {
        myPlayer.SetSelectedLRF(selectedLRF);

        foreach (Text infoText in LRFInfoTexts)
        {
            infoText.gameObject.SetActive(false);
        }
        LRFInfoTexts[selectedLRF].gameObject.SetActive(true);
    }


    //Changes the color of Tanks Materials 
    public void ChangeColorOfModel1(int colorCode)
    {

        switch (colorCode)
        {
            case 0:
                model1Material.color = new Color(0.2f, 0.1f, 0.5f);
                break;
            case 1:
                model1Material.color = new Color(0.1f, 0.5f, 0.5f);
                break;
            case 2:
                model1Material.color = new Color(0.5f, 0.4f, 0.4f);
                break;
        }
       
    }
    public void ChangeColorOfModel2(int colorCode)
    {
        switch (colorCode)
        {
            case 0:
                model2Material.color = new Color(0.2f, 0.1f, 0.5f);
                break;
            case 1:
                model2Material.color = new Color(0.1f, 0.5f, 0.5f);
                break;
            case 2:
                model2Material.color = new Color(0.5f, 0.4f, 0.4f);
                break;
        }
              
    }

    //Changes the color of Canaon material
    public void ChangeColorOfCanon(int colorCode)
    {

        switch (colorCode)
        {
            case 0:
                canonMaterial.color = new Color(0.2f, 0.1f, 0.5f);
                break;
            case 1:
                canonMaterial.color = new Color(0.1f, 0.5f, 0.5f);
                break;
            case 2:
                canonMaterial.color = new Color(0.5f, 0.4f, 0.4f);
                break;
            case 3:
                canonMaterial.color = new Color(0.8f, 0.3f, 0.1f);
                break;
            case 4:
                canonMaterial.color = new Color(0.05f, 0.8f, 0.3f);
                break;
            case 5:
                canonMaterial.color = new Color(0.8f, 0.8f, 0.4f);
                break;
        }

    }

    //Changes the color of LRF material
    public void ChangeColorOfLRF(int colorCode)
    {

        switch (colorCode)
        {
            case 0:
                lrfMaterial.color = new Color(0.2f, 0.1f, 0.5f);
                break;
            case 1:
                lrfMaterial.color = new Color(0.1f, 0.5f, 0.5f);
                break;
            case 2:
                lrfMaterial.color = new Color(0.5f, 0.4f, 0.4f);
                break;
            case 3:
                lrfMaterial.color = new Color(0.8f, 0.3f, 0.1f);
                break;
            case 4:
                lrfMaterial.color = new Color(0.05f, 0.8f, 0.3f);
                break;
            case 5:
                lrfMaterial.color = new Color(0.8f, 0.8f, 0.4f);
                break;
        }

    }

    //Set difficulty and change difficulty button interactions
    public void SelectDifficulty(int selectedDifficulty)
    {
        difficultyButtons[currDifficulty].interactable = true;
        currDifficulty = selectedDifficulty;
        difficultyButtons[currDifficulty].interactable = false;

    }

    //Changes Scene after Scene transition Animation
    public void StartGameSceneAfterAnim()
    {
        if (nameInputField.text != "")
        {
            canvasAnimator.SetTrigger("SceneChange");
            Invoke("StartGameScene", 4f);
        }
        else
        {
            canvasAnimator.SetTrigger("AlertNameTag");

        }
    }

    //Load the GameScene with the last data to send
    public void StartGameScene()
    {

      PlayerPrefs.SetString("playerName", nameInputField.text);
      PlayerPrefs.SetInt("selectedDifficulty", currDifficulty);
      SceneManager.LoadScene("GameScene");
       
    }

    //Close the game
    public void ExitGame()
    {
        Application.Quit();
    }
    
    //Change Sound option as mute or unmute
    public void ChangeSoundOption()
    {
        if(soundMuted == true)
        {
            soundMuted = false;
            soundButtonImgs[0].SetActive(true);
            soundButtonImgs[1].SetActive(false);
            buttonSound.mute = false;
            backgroundMusic.mute = false;
            canvasSound.mute = false;
            PlayerPrefs.SetInt("SoundOpt", 0);
        }
        else
        {

            soundMuted = true;
            soundButtonImgs[0].SetActive(false);
            soundButtonImgs[1].SetActive(true);
            buttonSound.mute = true;
            backgroundMusic.mute = true;
            canvasSound.mute = true;
            PlayerPrefs.SetInt("SoundOpt", 1);
        }

    }
}
