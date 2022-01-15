using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenuButtonHandler : MonoBehaviour
{
    [Header("Canvases")]
    public Canvas menuCanvas;
    [Space(5)]
    public Canvas saveSelectCanvas;
    [Space(5)]
    public Canvas settingsCanvas;
    [Space(5)]
    public Canvas creditsCanvas;

    [Header("Other")]
    public VideoPlayer creditsVideoPlayer;

    private LevelHandler levelHandler;

    void Start()
    {
        levelHandler = FindObjectOfType<LevelHandler>();

        menuCanvas.enabled = true;
        saveSelectCanvas.enabled = false;
        settingsCanvas.enabled = false;
        creditsCanvas.enabled = false;

        creditsVideoPlayer.Stop();
    }

    //Menu Buttons --------------------
    public void OnNewGame()
    {
        levelHandler.StartGame("Placeholder");
        //Start the game
    }

    public void OnLoadGame()
    {
        menuCanvas.enabled = false;
        saveSelectCanvas.enabled = true;
        //switch to save select canvas
    }

    public void OnSettings()
    {
        menuCanvas.enabled = false;
        settingsCanvas.enabled = true;
        //switch to settings canvas
    }

    public void OnCredits()
    {
        menuCanvas.enabled = false;
        creditsCanvas.enabled = true;
        creditsVideoPlayer.Play();
        //switch to credits canvas
    }

    public void OnExitGame()
    {
        Application.Quit();
        //quit the game
    }
    //--------------------

    //Save Select Buttons ---------------------

    public void OnSaveSelectBack()
    {
        saveSelectCanvas.enabled = false;
        menuCanvas.enabled = true;
        //go back to menu
    }

    //--------------------

    //Settings Buttons --------------------

    public void OnSettingsBack()
    {
        settingsCanvas.enabled = false;
        menuCanvas.enabled = true;
        //go back to menu
    }

    //--------------------

    //Credits Buttons --------------------

    public void OnCreditsBack()
    {
        creditsVideoPlayer.Stop();
        creditsCanvas.enabled = false;
        menuCanvas.enabled = true;
        //go back to menu
    }

    //--------------------
}
