
//This script is used for button actions in FinalScene.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{

    [SerializeField] AudioSource finalSceneAudio;
    [SerializeField] AudioSource buttonAudio;

    void Awake()
    {
        if (PlayerPrefs.GetInt("SoundOpt") == 1)
        {
            finalSceneAudio.mute = true;
            buttonAudio.mute = true;
        }
    }

    public void ReturnStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
