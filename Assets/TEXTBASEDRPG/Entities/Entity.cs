using System;
using EventSystem;
using EventSystem.TEXTBASEDRPG.Entities;
using UnityEngine;

namespace Namable
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entities/Entity")]
    public abstract class Entity : Namable, Interactable
    {
        public Dialogue dialogue;

        public int hp;

        public Inventory inventory;

        public Stats stats;

        public Entity(string name, Stats stats)
        {
            this.name = name;
            this.stats = stats;
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
                    imageFinishInfo.eventState = NAMABLE_EVENT_STATE.PICTURE_ONLY;
                    imageFinishInfo.buttonOptionString = name;
                    return imageFinishInfo;
                case NAMABLE_STATE.NAMED:
                    var namedInfo = CreateInstance<DialogueEventInfo>();
                    namedInfo.dialogue = imageDialogue;
                    namedInfo.dialogue.namable = this;
                    namedInfo.dialogue.current.insertText = name;
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