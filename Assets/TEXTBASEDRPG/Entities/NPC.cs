using System;
using EventSystem;
using Namable;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Entities/NPC")]
public class NPC : Entity
{
    public DialogueEventInfo dialogueEventInfo;

    public DialogueNode greeting;

    public NPC(string name, Stats stats, Dialogue dialogue, DialogueNode greeting) : base(name, stats)
    {
        this.dialogue = dialogue;
        this.greeting = greeting;
    }

    public override void Inspect()
    {
        throw new NotImplementedException();
    }

    public override DialogueEventInfo TalkTo()
    {
        return dialogueEventInfo;
    }

    public override void Take()
    {
        throw new NotImplementedException();
    }

    public override void Back()
    {
        throw new NotImplementedException();
    }
}