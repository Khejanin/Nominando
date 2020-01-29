﻿using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using TMPro;
using UnityEngine;

public class RegisterButtonScript : MonoBehaviour
{
    public TMP_InputField usernameInput,passwordInput,passwordConfirmInput;

    public LeanPulse notification;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LeanButton>().OnClick.AddListener(OnLoginClickEvent);
    }

    public void OnLoginClickEvent()
    {
        if (usernameInput.text.Length >= 4)
        {
            if (passwordInput.text.CompareTo(passwordConfirmInput.text) == 0)
            {
                if (passwordInput.text.Length >= 4)
                    StartCoroutine(APIHandler.getAPIHandler().Register(usernameInput.text, passwordInput.text));
                else
                {
                    NotificationTextScript.GetNotificationTextScript()
                        .SetNotificationTextAndShow("Passwords is too short!");
                }
            }
            else
            {
                NotificationTextScript.GetNotificationTextScript()
                    .SetNotificationTextAndShow("Passwords do not match!");
            }
        }
        else
        {
            NotificationTextScript.GetNotificationTextScript().SetNotificationTextAndShow("Username is too short!");
        }
    }

}
