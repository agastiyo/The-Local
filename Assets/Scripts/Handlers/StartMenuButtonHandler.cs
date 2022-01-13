using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenuButtonHandler : MonoBehaviour
{
    public MenuStartup startup;

    //Menu Buttons --------------------
    public void OnNewGame()
    {
        SceneManager.LoadScene(1);
        //Go to game scene
    }

    public void OnLoadGame()
    {
        startup.menuCanvas.enabled = false;
        startup.saveSelectCanvas.enabled = true;
        //switch to save select canvas
    }

    public void OnSettings()
    {
        startup.menuCanvas.enabled = false;
        startup.settingsCanvas.enabled = true;
        //switch to settings canvas
    }

    public void OnCredits()
    {
        startup.menuCanvas.enabled = false;
        startup.creditsCanvas.enabled = true;
        startup.creditsVideoPlayer.Play();
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
        startup.saveSelectCanvas.enabled = false;
        startup.menuCanvas.enabled = true;
        //go back to menu
    }

    //--------------------

    //Settings Buttons --------------------

    public void OnSettingsBack()
    {
        startup.settingsCanvas.enabled = false;
        startup.menuCanvas.enabled = true;
        //go back to menu
    }

    //--------------------

    //Credits Buttons --------------------

    public void OnCreditsBack()
    {
        startup.creditsVideoPlayer.Stop();
        startup.creditsCanvas.enabled = false;
        startup.menuCanvas.enabled = true;
        //go back to menu
    }

    //--------------------
}