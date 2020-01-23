using System.Collections;
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
        if(passwordInput.text.CompareTo(passwordConfirmInput.text) == 0) StartCoroutine(APIHandler.getAPIHandler().Register(usernameInput.text, passwordInput.text));
        else
        {
            NotificationTextScript.GetNotificationTextScript().SetNotificationTextAndShow("Passwords do not match!");
        }
    }

}
