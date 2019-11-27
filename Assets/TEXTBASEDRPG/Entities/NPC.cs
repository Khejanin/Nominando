using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{

    public NPC(string name, Stats stats, Dialogue dialogue, DialogueNode greeting) : base(name,stats)
    {
        this.dialogue = dialogue;
        this.greeting = greeting;
    }

    public Dialogue dialogue;

    public DialogueNode greeting;

    public DialogueNode talkTo()
    {
        return greeting;
    }

}
