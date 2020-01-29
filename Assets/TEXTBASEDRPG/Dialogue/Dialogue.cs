using System;
using EventSystem;
using Namable;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
[Serializable]
public class Dialogue : ScriptableObject
{
    public DialogueNode current;

    public DialogueNode first;

    [HideInInspector] public Namable.Namable namable;

    public bool juicy = false;

    [HideInInspector] public DialogueType dialogueType = DialogueType.NORMAL_DIALOGUE;

    public enum DialogueType
    {
        NORMAL_DIALOGUE,
        NAMABLE_DIALOGUE,
        IMAGE_DIALOGUE
    }

    public void ClearAllInsertTexts()
    {
        DialogueNode node = first;
        while (node != null)
        {
            if (node.canClear)
                node.insertText = "";
            node = node.next;
        }
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
            if (current.nameFromNamable)
            {
                return current.dialogueText.Replace("_NAME_", "<color=#0FF>" + current.nameFrom.name + "</color>");
            }

            if (!current.insertText.Equals(""))
                return current.dialogueText.Replace("_NAME_", "<color=#0FF>" + current.insertText + "</color>");
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
                if (next != null && next.insertText == "" && current.insertText != "")
                    next.insertText = current.insertText;
                current = next;
            }
            else throw new NoDialogueOptionSelected();

            {
            }
        }
        else if (current != null)
        {
            if (current.holdsEvent && current.info) EventSystem.EventSystem.FireEvent(current.GetEvent());
            if (current.isSaved)
            {
                string text = "";
                if (current.isGameMessage)
                {
                    text = "<b>" + "Game" + "</b>" + " : " + current.dialogueText.Replace("_NAME_",
                               "<color=#0FF>" + current.insertText + "</color>");
                }
                else
                {
                    if (!(namable.GetType() == typeof(Location)))
                    {
                        if (current.nameFrom)
                            text = "<b>" + namable.name + "</b>" + " : " + current.dialogueText.Replace("_NAME_",
                                       "<color=#0FF>" + current.nameFrom.name + "</color>");
                        else
                            text = "<b>" + namable.name + "</b>" + " : " +
                                   current.dialogueText.Replace("_NAME_",
                                       "<color=#0FF>" + current.insertText + "</color>");
                    }
                    else
                    {
                        text = "<b>" + current.dialogueText.Replace("_NAME_",
                                   "<color=#0FF>" + current.insertText + "</color>") + "</b>";
                    }
                }

                HistorySystem.GetHistorySystem().addText(text);
            }

            if (current.next != null && current.next.insertText == "" && current.insertText != "")
                current.next.insertText = current.insertText;
            current = current.next;
        }

        if (current == null)
        {
            ClearAllInsertTexts();
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

                var entityEvent = CreateInstance<EntityEventInfo>();
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