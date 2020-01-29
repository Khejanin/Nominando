using System.Collections;
using System.Collections.Generic;
using EventSystem;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonScript : MonoBehaviour
{

    public TextMeshProUGUI continueText;
    private LeanButton button;
    
    // Start is called before the first frame update
    void Start()
    {
        continueText.SetText("Continue as " + PlayerPrefs.GetString("user"));
        continueText.SetAllDirty();
        button = GetComponent<LeanButton>();
        button.OnClick.AddListener(OnClick);
    }

    void OnClick()
    {
        LoginEventInfo loginEvent = ScriptableObject.CreateInstance<LoginEventInfo>();
        loginEvent.eventState = LoginEventInfo.LoginEventState.LOGIN_SUCCESSFUL;
        EventSystem.EventSystem.FireEvent(loginEvent);
    }
}
