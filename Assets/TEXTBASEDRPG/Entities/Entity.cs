using System;
using System.Collections.Generic;
using EventSystem;
using EventSystem.TEXTBASEDRPG.Entities;
using UnityEngine;

namespace Namable
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entities/Entity")]
    public abstract class Entity : Namable, Interactable
    {
        public Dialogue dialogue;

        public List<Dialogue> dialogues = new List<Dialogue>();
        
        public int hp;

        public Inventory inventory;

        public Stats stats;

        public int dialogueState = 0;

        private bool imgSpecsInitialized = false;

        public Entity(string name, Stats stats)
        {
            this.name = name;
            this.stats = stats;
        }

        public new void Clear()
        {
            base.Clear();
            dialogueState = 0;
        }

        private void OnEnable()
        {
            /*name = "";
            image = null;
            namableState = NAMABLE_STATE.INITIAL;
            initImageSettings(256,455);*/
        }

        public override EventInfo GetEvent()
        {
            if (!imgSpecsInitialized)
            {
                imageWidth = 256;
                imageHeight = 512;
            }
            switch (namableState)
            {    
                case NAMABLE_STATE.COMPLETE:
                    var completeInfo = CreateInstance<EntityEventInfo>();
                    completeInfo.entity = this;
                    completeInfo.buttonOptionString = name;
                    completeInfo.EventInfoString = "This Event is an EntityEvent with " + name + " inside";
                    return completeInfo;
                case NAMABLE_STATE.IMAGE_DIALOGUE_FINISH:
                    var imageFinishInfo = CreateInstance<NamableEventInfo>();
                    imageFinishInfo.namable = this;
                    imageFinishInfo.buttonOptionString = "Give " + name + " a picture!";
                    imageFinishInfo.eventState = NAMABLE_EVENT_STATE.PICTURE_ONLY;
                    imageFinishInfo.buttonOptionString = name;
                    return imageFinishInfo;
                case NAMABLE_STATE.NAMED:
                    var namedInfo = CreateInstance<DialogueEventInfo>();
                    namedInfo.dialogue = imageDialogue;
                    namedInfo.dialogue.namable = this;
                    namedInfo.buttonOptionString = name;
                    namedInfo.dialogue.first.insertText = name;
                    namedInfo.dialogue.first.canClear = false;
                    namedInfo.juicy = imageDialogue.juicy;
                    namedInfo.dialogue.dialogueType = Dialogue.DialogueType.IMAGE_DIALOGUE;
                    return namedInfo;
                case NAMABLE_STATE.NAMABLE_DIALOGUE_FINISH:
                    var namableFinishInfo = CreateInstance<NamableEventInfo>();
                    namableFinishInfo.namable = this;
                    namableFinishInfo.eventState = NAMABLE_EVENT_STATE.NAME_ONLY;
                    namableFinishInfo.buttonOptionString = "???";
                    return namableFinishInfo;
                default:
                    var initialInfo = CreateInstance<DialogueEventInfo>();
                    initialInfo.dialogue = namableDialogue;
                    initialInfo.dialogue.namable = this;
                    initialInfo.juicy = namableDialogue.juicy;
                    initialInfo.dialogue.dialogueType = Dialogue.DialogueType.NAMABLE_DIALOGUE;
                    initialInfo.buttonOptionString = "???";
                    return initialInfo;
            }
        }

        public abstract void Inspect();
        public abstract DialogueEventInfo TalkTo();
        public abstract void Take();
        public abstract void Back();

        [Serializable]
        public class Stats
        {
            public int Arcana;
            public int Endurance;
            public int Intelligence;
            public int Speed;
            public int Strength;
        }
    }
}