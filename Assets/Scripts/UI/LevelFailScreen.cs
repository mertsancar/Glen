using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFailScreen : BaseScreen
{
    public Button againButton;
    public Button returnMainButton;
    public Button quitButton;

    public override void Prepare(object param)
    {
        EventManager.instance.TriggerEvent(EventName.LevelFail);
    }

    public void OnClickAgainButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickReturnMainButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickQuitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
