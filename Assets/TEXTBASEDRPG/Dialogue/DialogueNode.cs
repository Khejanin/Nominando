using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode : MonoBehaviour
{

    public string insertText;
    public string dialogueText;

    public DialogueNode(string text = "This Dialogue has not been written yet!")
    {
        setDialogueText(text);
    }

    public string setBothText(string text, string insert)
    {
        return dialogueText = string.Format(text, insert);
    }

    public string setInsertText(string insert)
    {
        return dialogueText = string.Format(dialogueText, insertText);
    }

    public string setDialogueText(string text = "This Dialogue has not been written yet!")
    {
        return dialogueText = string.Format(text, insertText);
    }

    public DialogueNode next;

    public DialogueNode continueDialogue() {
        return next;
    }

    public void Start()
    {
        dialogueText = string.Format(dialogueText, insertText);
    }

}
