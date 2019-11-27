using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour
{

    public DialogueNode first;
    public DialogueNode current;

    public Mode mode = Mode.Linear;

    public enum Mode
    {
        Linear,
        Choice
    }

    public string getDialogue()
    {
        if (current != null)
            return current.dialogueText;
        else return "Dialogue End";
    }

    public string continueDialogue(DialogueOption option)
    {
        Debug.Log(current);
        if (current is DialogueNodeSplit)
        {
            Debug.Log("Is DialogueNodeSplit");
            if (option != null)
                current = (current as DialogueNodeSplit).chooseOption(option);
            else throw new NoDialogueOptionSelected();
        }
        else if(current!=null)current = current.next;
        if (current != null) return current.dialogueText;
        else return "Dialogue End";
    }

}

public class NoDialogueOptionSelected : System.Exception
{
    public NoDialogueOptionSelected(string msg = "You have not selected any Dialogue Message!") : base(msg)
    {
        
    }
}
