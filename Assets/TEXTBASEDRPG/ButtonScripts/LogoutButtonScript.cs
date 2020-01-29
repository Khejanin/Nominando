using System.Collections;
using System.Collections.Generic;
using EventSystem;
using Lean.Gui;
using UnityEngine;

public class LogoutButtonScript : MonoBehaviour
{
    private LeanButton button;
    public bool doesLogout = true;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<LeanButton>();
        button.OnClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (doesLogout)
        {
            PlayerPrefs.SetString("token", "");
            LoginEventInfo eventInfo = ScriptableObject.CreateInstance<LoginEventInfo>();
            eventInfo.eventState = LoginEventInfo.LoginEventState.LOGOUT;
            EventSystem.EventSystem.FireEvent(eventInfo);
        }
        else
        {
            LoginEventInfo eventInfo = ScriptableObject.CreateInstance<LoginEventInfo>();
            eventInfo.eventState = LoginEventInfo.LoginEventState.BACK_TO_MENU;
            EventSystem.EventSystem.FireEvent(eventInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
