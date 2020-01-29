using System;
using EventSystem;
using Namable;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Entities/NPC")]
public class NPC : Entity
{

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
        DialogueEventInfo dialogueEventInfo = CreateInstance<DialogueEventInfo>();
        if (dialogueState < dialogues.Count)
        {
            dialogueEventInfo.dialogue = dialogues[dialogueState];
        }
        else dialogueEventInfo.dialogue = dialogues[0];
        dialogueEventInfo.dialogue.namable = this;
        dialogueEventInfo.eventType = DIALOGUE_EVENT_TYPE.StartDialogue;
        dialogueEventInfo.juicy = juicy;
        dialogueEventInfo.SetDefaultButtonStrings();
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

    public override APIHandler.EntityUploadJSON getUploadData()
    {
        APIHandler.EntityUploadJSON upload = new APIHandler.EntityUploadJSON();
        upload.isNamable = true;
        upload.Name = name;
        upload.State = namableState.ToString();
        upload.UniqueId = uniqueID;
        upload.dialogueState = dialogueState;
        return upload;
    }
}