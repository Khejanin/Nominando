using System.Collections;
using System.Collections.Generic;
using TEXTBASEDRPG.EventSystem;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    public DialogueSystem dialogueSystem;

    public void HandleEvent(EventSystem eventSystemMaster)
    {
        EventSystem.EventSystemMaster eventSystem = eventSystemMaster.eventSystemMaster;
        if(eventSystem.interaction == EventSystem.InteractionType.Talk)
        {
            if (eventSystem.e.eventType == EventSystem.EventType.DialogueBegin)
                dialogueSystem.setCurrentDialogue(eventSystem.e.dialogueEvent.dialogueEntity.dialogue);
            else if (eventSystem.e.eventType == EventSystem.EventType.DialogueContinue)
                dialogueSystem.continueDialogue();
            else dialogueSystem
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
