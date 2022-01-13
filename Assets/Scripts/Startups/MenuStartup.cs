using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuStartup : MonoBehaviour
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

    void Start()
    {
        menuCanvas.enabled = true;
        saveSelectCanvas.enabled = false;
        settingsCanvas.enabled = false;
        creditsCanvas.enabled = false;

        creditsVideoPlayer.Stop();
    }
}
