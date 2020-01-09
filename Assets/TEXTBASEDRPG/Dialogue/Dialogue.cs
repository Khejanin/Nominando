using System;
using EventSystem;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
[Serializable]
public class Dialogue : ScriptableObject
{
    public DialogueNode current;

    public DialogueNode first;

    [HideInInspector]
    public Namable.Namable namable;

    [HideInInspector]
    public DialogueType dialogueType = DialogueType.NORMAL_DIALOGUE;

    public enum DialogueType
    {
        NORMAL_DIALOGUE,
        NAMABLE_DIALOGUE,
        IMAGE_DIALOGUE
    }

    private void Awake()
    {
        Reset();
    }

    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        current = first;
    }

    public string GetDialogue()
    {
        if (current != null)
        {
            if (!current.insertText.Equals(""))
                return current.dialogueText.Replace("_NAME_", current.insertText);
            return current.dialogueText;
        }
        else
        {
            return "";
        }
    }

    public bool ContinueDialogue(DialogueNode next)
    {
        if (current is DialogueNodeSplit)
        {
            if (next != null)
            {
                if(next != null)
                    next.insertText = current.insertText;
                current = next;
            }
            else throw new NoDialogueOptionSelected();
            {
                
            }
        }
        else if (current != null)
        {
            if(current.next != null)
                current.next.insertText = current.insertText;
            current = current.next;
        }
        if (current == null)
        {
            if (dialogueType == DialogueType.NAMABLE_DIALOGUE)
            {
                StopDialogueEvent();
                namable.namableState = Namable.Namable.NAMABLE_STATE.NAMABLE_DIALOGUE_FINISH;
                EventSystem.EventSystem.FireEvent(namable.GetEvent());
            }

            if (dialogueType == DialogueType.IMAGE_DIALOGUE)
            {
                StopDialogueEvent();
                namable.namableState = Namable.Namable.NAMABLE_STATE.IMAGE_DIALOGUE_FINISH;
                EventSystem.EventSystem.FireEvent(namable.GetEvent());
            }

            if (dialogueType == DialogueType.NORMAL_DIALOGUE)
            {
                current = first;
                
                StopDialogueEvent();
                
                var entityEvent =  CreateInstance<EntityEventInfo>();
                entityEvent.entityEventType = ENTITY_EVENT_TYPE.ENTITY_STOP;
                
                EventSystem.EventSystem.FireEvent(entityEvent);

                var locationEvent = CreateInstance<LocationEventInfo>();
                locationEvent.location = Game.currentLocation;
                
                EventSystem.EventSystem.FireEvent(locationEvent);
                return false;
            }
        }
        return true;
    }

    public void StopDialogueEvent()
    {
        var dialogueEvent = CreateInstance<DialogueEventInfo>();
        dialogueEvent.dialogue = this;
        dialogueEvent.eventType = DIALOGUE_EVENT_TYPE.EndDialogue;
               
        EventSystem.EventSystem.FireEvent(dialogueEvent);
    }
}

public class NoDialogueOptionSelected : Exception
{
    public NoDialogueOptionSelected(
        string msg = "You have not selected any Dialogue Message! | CHECK IF YOUR DATA IS LINKED PROPERLY") : base(msg)
    {
    }
}