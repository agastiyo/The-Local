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

    public void TweenIn() //slide in
    {
        panel.rectTransform.anchoredPosition = new Vector2(0f,-2000f);
        Tween tween = panel.rectTransform.DOAnchorPosY(0f, duration).SetUpdate(true).SetEase(Ease.OutSine);
        tween.onComplete += () => isLoading = true;
    }

    public void TweenOut() //slide out
    {
        Tween tween = panel.rectTransform.DOAnchorPosY(-2000f, duration).SetUpdate(true).SetEase(Ease.InSine);
        tween.onComplete += () => isLoading = false;
    }
}
