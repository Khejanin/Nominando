using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TEXTBASEDRPG.EventSystem
{
    
    public class EventSystem : MonoBehaviour
    {
        public EventSystemMaster eventSystemMaster;

        public string text;
        
        [System.Serializable]
        public class EventSystemMaster
        {
            public bool autosave;
            public bool doOnce;
            public InteractionType interaction;
            public Event e;
        }

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
            public NamableEvent namableEvent;
            public InteractEvent interactEvent;
            public DialogueEvent dialogueEvent;
            public DialogueContinueEvent dialogueContinueEvent;
            public DialogueChooseOptionEvent dialogueChooseOptionEvent;
            public GoEvent goEvent;
        }

        [System.Serializable]
        public class NamableEvent
        {
            public Namable namable;
        }

        [System.Serializable]
        public class InteractEvent
        {
            public Entity interactionEntity;
        }

        [System.Serializable]
        public class DialogueEvent
        {
            public Entity dialogueEntity;
        }

        [System.Serializable]
        public class DialogueContinueEvent
        {
            public Dialogue dialogueContinue;
        }

        [System.Serializable]
        public class DialogueChooseOptionEvent
        {
            public Dialogue dialogueContinue;
            public DialogueNode option;
        }

        [System.Serializable]
        public class GoEvent
        {
            //TODO
        }

    }
}
