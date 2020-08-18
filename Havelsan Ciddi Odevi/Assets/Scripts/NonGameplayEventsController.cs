
//This script controls the events before gameplay started and after gameplay endeded in GameScene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonGameplayEventsController : MonoBehaviour
{
    bool soundMuted = false;
    public bool gameIsPaused = false;

    [SerializeField] GameObject[] GameplayElements;
    [SerializeField] PlayerController playerController;
    [SerializeField] Gun gun;

    [Header("GUI Elements")]
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject pressText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject canvasGameUI;
    [SerializeField] GameObject[] soundButtonImgs;

    [Header("Audio")]
    [SerializeField] AudioSource AmbientSource;
    [SerializeField] AudioSource shipTurnSource;
    [SerializeField] AudioSource shipSource;

    //Check sound selection before game starts.
    void Awake()
    {
        if(PlayerPrefs.GetInt("SoundOpt") == 1)
        {
            ChangeSoundOption();
        }

    }

    // Disable all gameplay elements before game started.
    void Start()
    {
        foreach (GameObject element in GameplayElements)
        {
            element.SetActive(false);
        }
    }

    // Gets Input to start the game.
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(playerController.gameStarted == false)
                StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(playerController.gameStarted == true)
            {
                if (gameIsPaused == false)
                {
                    PauseGame();
                }
                else if (gameIsPaused == true)
                {
                    ResumeGame();
                }
            }
        }

    }

    //Start game by activate all game elements and call wave start function depends on difficulty selection of player.
    public void StartGame()
    {
        GetComponent<Animator>().enabled = false;
        pressText.SetActive(false);

        foreach (GameObject element in GameplayElements)
        {
            element.SetActive(true);
        }

        playerController.gameStarted = true;
        WaveController.Instance.StartQueueByDifficulty(PlayerPrefs.GetInt("selectedDifficulty"));

    }

    //End game with win condition.
    public void FinishGameWithWin()
    {
        playerController.gameStarted = false;
        Debug.Log("controller:" + playerController.gameTime);
        GameInfoKeeper.Instance.SetEndTime(playerController.gameTime);
        winPanel.SetActive(true);
    }

    // End game with lose condition.
    public void FinishGameWithLose()
    {
        playerController.gameStarted = false;
        Debug.Log("controller:" + playerController.gameTime);
        GameInfoKeeper.Instance.SetEndTime(playerController.gameTime);
        losePanel.SetActive(true);

    }
    
    //Change Sound option as mute or unmute
    public void ChangeSoundOption()
    {
        if (soundMuted == true)
        {
            soundMuted = false;
            soundButtonImgs[0].SetActive(true);
            soundButtonImgs[1].SetActive(false);
            AmbientSource.mute = false;
            shipTurnSource.mute = false;
            shipSource.mute = false;
            PlayerPrefs.SetInt("SoundOpt", 0);
        }
        else
        {

            soundMuted = true;
            soundButtonImgs[0].SetActive(false);
            soundButtonImgs[1].SetActive(true);
            AmbientSource.mute = true;
            shipTurnSource.mute = true;
            shipSource.mute = true;
            PlayerPrefs.SetInt("SoundOpt", 1);
        }

    }

    //Pause game and open menu
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        gun.gamePaused = true;
    }

    //Close Menu and resume game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gun.gamePaused = false;
    }
}
