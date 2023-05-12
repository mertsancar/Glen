using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavesScreen : BaseScreen
{
    public Button backButton;
    public override void Prepare(object param)
    {
        backButton.onClick.AddListener(HideScreen);
    }
}
