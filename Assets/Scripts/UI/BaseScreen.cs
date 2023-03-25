using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseScreen : MonoBehaviour
{
    public virtual void Awake()
    {
        
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }
    
    public virtual void Prepare(object param) {}
    
    public virtual void OnEnable()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f);
    }
    
    public void OnCloseButtonClicked()
    {
        HideScreen();
    }
    
    public virtual void HideScreen()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.5f).OnComplete(OnHideScreenComplete);
    }

    public virtual void OnHideScreenComplete()
    {
        gameObject.SetActive(false);
        EventManager.instance.TriggerEvent(EventName.ScreenClosed, GetType());
    }
}
