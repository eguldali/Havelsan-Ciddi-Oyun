
//This script is used to set Player according the selections from StartScene & control shipbased actions(change camera mode, turn ship left & right, enemy hit, keep game time) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterModel[] characterModelsForPlayer;
    [SerializeField] NonGameplayEventsController nonGameplayEvents;
    [SerializeField] Gun playerGun;
    [SerializeField] LrfView lrfView;
    [SerializeField] float turnSpeed;

    public bool gameStarted = false;
    public float gameTime = 0f;

    public bool inLrfMode = false;

    [Header("Cameras")]
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject lrfCamera;


    [Header("GUI Elements")]
    [SerializeField] Text nameText;


    [Header("Audio")]
    [SerializeField] AudioSource shipMovementSource;
    [SerializeField] AudioSource shipAudioSource;
    public AudioClip shipDestroyedSound;

    // Start GameScene with characters selections.
    void Start()
    {
        SetUpPlayer();
    }
    

    // Keeps Game Time & gets input for ship turn and game view change
    void Update()
    {
        if (gameStarted == true)
        {
            if (inLrfMode == false)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                    if (shipMovementSource.isPlaying == false)
                        shipMovementSource.Play();
                }

                else if (Input.GetKey(KeyCode.Q))
                {
                    transform.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);
                    if (shipMovementSource.isPlaying == false)
                        shipMovementSource.Play();
                }
                else
                {
                    shipMovementSource.Stop();
                }
            }
            else
            {
                shipMovementSource.Stop();
            }
            gameTime += Time.deltaTime;

            if (Input.GetMouseButtonDown(1))
            {
                ChangeViewMode();
            }


        }
    }

    // Set Player's selections
    public void SetUpPlayer()
    {
        SetPlayerGraphics();
        nameText.text = PlayerPrefs.GetString("playerName");
        
    }

    // Set Player's model
    void SetPlayerGraphics()
    {
        foreach (CharacterModel obj in characterModelsForPlayer)
        {
            obj.gameObject.SetActive(false);
        }

        characterModelsForPlayer[PlayerPrefs.GetInt("Character")].gameObject.SetActive(true);
        characterModelsForPlayer[PlayerPrefs.GetInt("Character")].ShowSelectedCharacter();
    }

    // Call lose function when an enemy hit player
    public void GotHitFromEnemy()
    {
        nonGameplayEvents.FinishGameWithLose();
        
        if (PlayerPrefs.GetInt("SoundOpt") == 0)
        {
            shipAudioSource.clip = shipDestroyedSound;
            shipAudioSource.Play();
        }

    }

    //Change between LRF Camera mode & Normal Camera mode
    void ChangeViewMode()
    {
        if(inLrfMode == false)
        {
            inLrfMode = true;
            playerCamera.SetActive(false);
            lrfCamera.SetActive(true);
            lrfView.gameObject.SetActive(true);
            playerGun.inLrfMode = true;
        }
        else
        {
            inLrfMode = false;
            playerCamera.SetActive(true);
            lrfCamera.SetActive(false);
            lrfView.gameObject.SetActive(false);
            playerGun.inLrfMode = false;


        }
    }

}
