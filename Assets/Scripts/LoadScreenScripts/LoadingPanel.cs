using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingPanel : MonoBehaviour
{
    public Image panel;
    [Range(0.1f,1f)]
    public float duration;

    [HideInInspector]
    public bool isLoading = false;

    private Color color;
    private Color alphaCol;

    // Start is called before the first frame update
    void Start()
    {
        color = panel.color;
        alphaCol = new Color(0f, 0f, 0f, 0f);
    }

    public void TweenIn() 
    {
        
    }
}
