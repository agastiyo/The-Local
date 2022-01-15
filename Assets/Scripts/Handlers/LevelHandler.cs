using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene("StartMenu"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //DON'T delete these two! StartGame() is needed to run StartGameAsync() on the Start Menu Button Handler!
    public void StartGame(string startingLevel) 
    {
        StartCoroutine(StartGameAsync(startingLevel));
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
        AsyncOperation showLoadScreen = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        yield return new WaitUntil(() => showLoadScreen.isDone);
        //add loading effects here
        
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
    }
    private IEnumerator FinishLoading()
    {
        //add loading effects here
        AsyncOperation hideLoadScreen = SceneManager.UnloadSceneAsync("LoadingScreen");
        yield return new WaitUntil(() => hideLoadScreen.isDone);

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
