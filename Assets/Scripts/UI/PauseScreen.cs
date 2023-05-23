using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : BaseScreen
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnMainButton;
    [SerializeField] private Button quitButton;

    public override void Prepare(object param)
    {
        
        resumeButton.onClick.AddListener(OnClickResumeButton);
        returnMainButton.onClick.AddListener(OnClickReturnMainButton);
        quitButton.onClick.AddListener(() => UnityEditor.EditorApplication.isPlaying = false);

    }

    private void OnClickResumeButton()
    {
        HideScreen();
    }

    private void OnClickReturnMainButton()
    {
        SceneManager.LoadScene("Main");
    }

    public override void OnDisable()
    {
        resumeButton.onClick.RemoveAllListeners();
        returnMainButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}
