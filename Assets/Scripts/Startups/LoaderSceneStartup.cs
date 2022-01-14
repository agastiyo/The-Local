using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene("Player"));
        StartCoroutine(LoadScene("Placeholder"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LoadUnloadScenes(string sceneToLoad, string sceneToUnload) {
        yield return StartCoroutine(UnloadScene(sceneToUnload));
        yield return StartCoroutine(LoadScene(sceneToLoad));
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
