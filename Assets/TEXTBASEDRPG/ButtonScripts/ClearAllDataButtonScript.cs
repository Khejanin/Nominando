using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;

public class ClearAllDataButtonScript : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<LeanButton>().OnClick.AddListener(OnClickEvent);
    }

    private void OnClickEvent()
    {
        APIHandler.getAPIHandler().ClearData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
