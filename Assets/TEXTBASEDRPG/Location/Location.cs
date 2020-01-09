using System.Collections.Generic;
using EventSystem;
using UnityEngine;

namespace Namable
{
    [CreateAssetMenu(fileName = "New Location", menuName = "Entities/Location")]
    public class Location : Namable
    {

        public List<IEventHolder> eventHolders = new List<IEventHolder>();
        
        public List<Entity> entities = new List<Entity>();
        public List<Location> locations = new List<Location>();
        public List<Action> actions = new List<Action>();

        public int hideEntitiesNr, hideLocationsNr, hideActionsNr;

        private static string path = "/img/";
        
        public override EventInfo GetEvent()
        {
            /*var info = CreateInstance<LocationEventInfo>();
            info.location = this;
            evaluateEventHolders();
            info.buttonOptionString = name;
            return info;*/
            /*if (namableState == NAMABLE_STATE.NAMED && imageDialogue == null){
                namableState = NAMABLE_STATE.IMAGE_DIALOGUE_FINISH;
            }*/
            switch (namableState)
            {
                case NAMABLE_STATE.COMPLETE:
                    var info = CreateInstance<LocationEventInfo>();
                    info.location = this;
                    EvaluateEventHolders();
                    info.buttonOptionString = name;
                    return info;
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

        public void EvaluateEventHolders()
        {
            eventHolders.Clear();
            for (int i = 0; i < entities.Count-hideEntitiesNr; i++)
            {
                eventHolders.Add(entities[i]);   
            }
            for (int i = 0; i < locations.Count-hideLocationsNr; i++)
            {
                eventHolders.Add(locations[i]);  
            }
            for (int i = 0; i < actions.Count-hideActionsNr; i++)
            {
                eventHolders.Add(actions[i]);
            }
        }

        private void Awake()
        {
            EvaluateEventHolders();
        }

        private void OnValidate()
        {
            EvaluateEventHolders();
        }

        private void OnEnable()
        {
            EvaluateEventHolders();
            InitImageSettings(1920,1080);
        }

        public void loadData()
        {
            Resources.Load(path);    
        }
        
        public void saveData()
        {
            
        }
        
    }
}