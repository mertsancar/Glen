using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    void Awake()
    {
        EventManager.instance.TriggerEvent(EventName.ShowScreenRequested, typeof(MenuScreen), null);
    }

}
