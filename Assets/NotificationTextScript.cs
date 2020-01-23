using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using Lean.Transition;
using UnityEngine;
using UnityEngine.UI;

public class NotificationTextScript : MonoBehaviour
{

    [HideInInspector]
    public Text text;

    private LeanPulse notificationPulse;

    private static NotificationTextScript nts;

    // Start is called before the first frame update
    void Start()
    {
        nts = this;
        text = gameObject.GetComponentInChildren<Text>();
        notificationPulse = gameObject.GetComponent<LeanPulse>();
    }

    public static NotificationTextScript GetNotificationTextScript()
    {
        return nts;
    }

    public void SetNotificationTextAndShow(string text)
    {
        this.text.text = text;
        notificationPulse.Pulse();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
