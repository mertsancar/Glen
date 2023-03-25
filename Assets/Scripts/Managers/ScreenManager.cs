using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.instance.AddListener(EventName.ShowScreenRequested, ShowScreenRequested);
    }
    
    private void ShowScreenRequested(object eventParams, object param)
    {
        ShowScreen((Type)eventParams, param);
    }
    
    public void ShowScreen(GameObject screen, object param)
    {
        screen.SetActive(true);
        screen.GetComponent<BaseScreen>().Prepare(param);
        EventManager.instance.TriggerEvent(EventName.ScreenShown, GetType());
    }
    
    private T ShowScreen<T>(object param) where T : BaseScreen
    {
        var screen = FindObjectOfType<T>();
        ShowScreen(screen.gameObject, param);
        return screen;
    }
    
    public Component ShowScreen(Type t, object param)
    {
        var screens = (BaseScreen[])Resources.FindObjectsOfTypeAll(t);
        foreach (var baseScreenController in screens)
        {
            var activeScene = SceneManager.GetActiveScene();
            if (baseScreenController.gameObject.scene == activeScene)
            {
                ShowScreen(baseScreenController.gameObject, param);
                return baseScreenController;
            }
        }
        return null;
    }
    
    private void HideScreenComplete()
    {
        HideAllScreens();
    }
    
    public void HideAllScreens()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    private void HideScreen(GameObject screen)
    {
        var canvasGroup = screen.GetComponent<CanvasGroup>();
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.5f);
    }
}
