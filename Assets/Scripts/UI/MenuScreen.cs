using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button savesButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    public override void Prepare(object param)
    {
        newGameButton.onClick.AddListener(OnClickNewGameButton);
        savesButton.onClick.AddListener(OnClickSavesButton);
        quitButton.onClick.AddListener(() => UnityEditor.EditorApplication.isPlaying = false);
    }

    private void OnClickNewGameButton()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnClickSavesButton() 
    {
        EventManager.instance.TriggerEvent(EventName.ShowScreenRequested, typeof(SavesScreen), null);
    }
}
