using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TEXTBASEDRPG.EventSystem
{
    [System.Serializable]
    public class MasterEvent
    {
        public bool autosave;
        public bool doOnce;
        public bool isDone;
        public InteractionType interaction;
        public Event e;

        public enum InteractionType
        {
            Talk,
            Trade,
            Fight,
            Interact,
            Discover,
            Go
        }

        public enum EventType
        {
            Namable,
            Interact,
            DialogueBegin,
            DialogueContinue,
            DialogueChooseOption,
            Go
        }

        [System.Serializable]
        public class Event
        {
            public EventType eventType;
        }

        [System.Serializable]
        public class NamableEvent : Event
        {
            public Namable namable;
        }

        [System.Serializable]
        public class InteractEvent : Event
        {
            public Entity interactionEntity;
        }

        [System.Serializable]
        public class DialogueStartEvent : Event
        {
            public Dialogue dialogue;

            public DialogueStartEvent()
            {
                eventType = EventType.DialogueBegin;
            }
        }

        [System.Serializable]
        public class DialogueContinueEvent : Event
        {
            public Dialogue dialogueContinue;

            public DialogueContinueEvent()
            {
                eventType = EventType.DialogueContinue;
            }
        }

        [System.Serializable]
        public class DialogueChooseOptionEvent : Event
        {
            public Dialogue dialogueContinue;
            public DialogueOption option;

            public DialogueChooseOptionEvent()
            {
                eventType = EventType.DialogueChooseOption;
            }
        }

        [System.Serializable]
        public class GoEvent : Event
        {
            //TODO
        }

    }
}
