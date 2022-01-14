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
        StartCoroutine(LoadUnloadScenes("LoaderScene", "StartMenu"));
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

    private IEnumerator LoadUnloadScenes(string sceneToLoad, string sceneToUnload) {
        yield return StartCoroutine(LoadScene(sceneToLoad));
        yield return StartCoroutine(UnloadScene(sceneToUnload));
    }

    private IEnumerator LoadScene(string sceneToLoad) {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadScene.isDone);
    }

    private IEnumerator UnloadScene(string sceneToUnload) {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneToUnload);
        yield return new WaitUntil(() => unloadScene.isDone);
    }
}