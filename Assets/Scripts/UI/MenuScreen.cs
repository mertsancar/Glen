using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button savesButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    public override void Prepare(object param)
    {
        quitButton.onClick.AddListener(Application.Quit);
    }
}
