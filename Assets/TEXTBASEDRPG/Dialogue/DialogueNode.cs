using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New DialogueNode", menuName = "Dialogue/LinearNode")]
public class DialogueNode : ScriptableObject
{
    public string dialogueText;
    public string insertText;
    public DialogueNode next;
}