using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New DialogueEventInfo", menuName = "EventSystem/DialogueEventInfo")]
    public class DialogueEventInfo : EventInfo
    {
        public Dialogue dialogue;

        public DIALOGUE_EVENT_TYPE eventType;
        public DialogueNode node;

        public bool enableCustomOption = false;

        public DialogueEventInfo(Dialogue dialogue, DialogueNode node, DIALOGUE_EVENT_TYPE eventType)
        {
            this.dialogue = dialogue;
            this.node = node;
            this.eventType = eventType;
            EventInfoString = "Dialogue Event Fired";
        }

        public void Validate()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            if (!enableCustomOption)
            {
                switch (eventType)
                {
                    case DIALOGUE_EVENT_TYPE.StartDialogue:
                        buttonOptionString = "Talk To";
                        break;
                    case DIALOGUE_EVENT_TYPE.ContinueDialogue:
                        if (node == null) buttonOptionString = "Continue";
                        break;
                    case DIALOGUE_EVENT_TYPE.EndDialogue:
                        buttonOptionString = "Goodbye!";
                        break;
                }
            }
        }
    }
}