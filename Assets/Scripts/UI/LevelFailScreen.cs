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
        GameController.instance.gameOver = true;
    }

    public void OnClickAgainButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickReturnMainButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
