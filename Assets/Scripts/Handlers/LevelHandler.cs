using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    private LoadingPanel loadingPanel;  

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene("StartMenu"));
    }

    public void LoadIfNotLoaded() 
    {

    }

    public void Load(string sceneToLoad)
    {
        StartCoroutine(LoadScene(sceneToLoad));
    }

    public void Unload(string sceneToUnload)
    {
        StartCoroutine(UnloadScene(sceneToUnload));
    }

    //DON'T delete these two! StartGame() is needed to run StartGameAsync() on the Start Menu Button Handler!
    public void StartGame(string startingLevel) 
    {
        Debug.Log("Starting game!");
        StartCoroutine(StartGameAsync(startingLevel));
        Debug.Log("Started game!");
    }
    private IEnumerator StartGameAsync(string startingLevel) 
    {
        yield return StartCoroutine(StartLoading());
        yield return StartCoroutine(LoadScene("Player"));
        yield return StartCoroutine(LoadScene(startingLevel));
        yield return StartCoroutine(UnloadScene("StartMenu"));
        yield return StartCoroutine(FinishLoading());
    }

    // Base Functions --------------------------------
    private IEnumerator StartLoading() 
    {
        //Load the Loading Screen
        AsyncOperation showLoadScreen = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        yield return new WaitUntil(() => showLoadScreen.isDone);
        
        //Tween the panel in and wait till done
        loadingPanel = FindObjectOfType<LoadingPanel>();
        loadingPanel.TweenIn();
        yield return new WaitUntil(() => loadingPanel.isLoading);
        
        //Stop the clock
        Time.timeScale = 0f;
    }
    private IEnumerator FinishLoading()
    {
        //Tween the panel out and wait till done
        loadingPanel.TweenOut();
        yield return new WaitUntil(() => !loadingPanel.isLoading);

        //Unload the Loading Screen
        AsyncOperation hideLoadScreen = SceneManager.UnloadSceneAsync("LoadingScreen");
        yield return new WaitUntil(() => hideLoadScreen.isDone);

        //Restart the clock
        Time.timeScale = 1f;
    }
    private IEnumerator LoadScene(string sceneToLoad) 
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadScene.isDone);
    }
    private IEnumerator UnloadScene(string sceneToUnload) 
    {
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneToUnload);
        yield return new WaitUntil(() => unloadScene.isDone);
    }
}
