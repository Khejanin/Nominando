using TEXTBASEDRPG.EventSystem;
using UnityEngine;

public class EventHandler
{

    private static EventHandler _eventHandler;
    
    public DialogueSystem dialogueSystem;
    
    public void HandleEvent(MasterEvent masterEvent)
    {
        if (masterEvent != null)
        {
            if (masterEvent.autosave)
            {
                //@TODO SAVE THE GAME
                Debug.Log("Game Saved");
            }

            if (masterEvent.interaction == MasterEvent.InteractionType.Talk)
            {
                if (masterEvent.e.eventType == MasterEvent.EventType.DialogueBegin)
                {
                    MasterEvent.DialogueStartEvent dialogueStartEvent = masterEvent.e as MasterEvent.DialogueStartEvent;
                    dialogueSystem.setCurrentDialogue(dialogueStartEvent.dialogue);
                }

                else if (masterEvent.e.eventType == MasterEvent.EventType.DialogueContinue)
                {
                    MasterEvent.DialogueContinueEvent dialogueContinueEvent =
                        masterEvent.e as MasterEvent.DialogueContinueEvent;
                    dialogueSystem.continueDialogue(null);
                }

                else if (masterEvent.e.eventType == MasterEvent.EventType.DialogueChooseOption)
                {
                    MasterEvent.DialogueChooseOptionEvent dialogueContinueEvent =
                        masterEvent.e as MasterEvent.DialogueChooseOptionEvent;
                    if (dialogueContinueEvent.dialogueContinue == dialogueSystem.getCurrentDialogue())
                        dialogueSystem.continueDialogue(dialogueContinueEvent.option);
                    else
                    {
                        Debug.Log("An option for a wrong Dialogue has been triggered! See In EventHandler");
                    }
                }
            }

            if (masterEvent.doOnce)
            {
                masterEvent.isDone = true;
            }
        }
        else
        {
            Debug.Log("Passed MasterEvent to handle was NULL !");
        }
    }

    private EventHandler()
    {
        
    }

    public static EventHandler getEventHandler()
    {
        if (_eventHandler != null)
        {
            return _eventHandler;
        }
        else return _eventHandler = new EventHandler();
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
