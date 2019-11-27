using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeSplit : DialogueNode
{

    public DialogueOptions options;

    public DialogueNode chooseOption(DialogueOption option)
    {
        return option.next;
    }

}
