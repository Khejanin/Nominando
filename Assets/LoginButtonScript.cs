using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using TMPro;
using UnityEngine;

public class LoginButtonScript : MonoBehaviour
{

    public TMP_InputField usernameInput,passwordInput;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LeanButton>().OnClick.AddListener(OnLoginClickEvent);
    }

    public void OnLoginClickEvent()
    {
        StartCoroutine(APIHandler.getAPIHandler().Login(usernameInput.text, passwordInput.text));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
