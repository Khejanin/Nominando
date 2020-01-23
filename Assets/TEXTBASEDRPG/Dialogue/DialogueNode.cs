using System;
using EventSystem;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New DialogueNode", menuName = "Dialogue/LinearNode")]
public class DialogueNode : ScriptableObject, IEventHolder
{
    public string dialogueText;
    public string insertText;
    public DialogueNode next;
    public bool holdsEvent;

    public Namable.Namable nameFrom;
    public bool nameFromNamable = false;

    public bool canClear = true;

    public EventInfo info;

    public EventInfo GetEvent()
    {
        return info;
    }
}