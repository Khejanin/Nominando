using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class ActionPanelScript : MonoBehaviour
{

    private static ButtonEventHandler[] buttons = new ButtonEventHandler[4];

    private void Start()
    {
        for (var i = 0; i < 4; i++)
            buttons[i] = GameObject.Find("Option" + i).GetComponent<ButtonEventHandler>();
    }

    public static void enableButtons(bool enable)
    {
        foreach (var button in buttons)
        {
            button.SetInteractable(enable);
        }
    }
}
