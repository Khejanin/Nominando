using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public static class LocationSystem
{
    //I dont want this to be a singleton

    //private static LocationSystem _locationSystem;
    private static readonly GameObject locationPanel;
    private static readonly Image locationPanelImage;

    private static readonly ButtonEventHandler[] _buttonEventHandlers = new ButtonEventHandler[4];

    static LocationSystem()
    {
        locationPanel = GameObject.Find("LocationPanel");
        locationPanelImage = locationPanel.GetComponent<Image>();
        EventSystem.EventSystem.RegisterListener<MasterEventInfo>(MasterEvent);
        EventSystem.EventSystem.RegisterListener<LocationEventInfo>(LocationEvent);
        Instantiate();
    }

    public static void Start()
    {
    }

    private static void Instantiate()
    {
        for (var i = 0; i < 4; i++)
            _buttonEventHandlers[i] = GameObject.Find("Option" + i).GetComponent<ButtonEventHandler>();
    }

    /*public static LocationSystem GetLocationSystem()
    {
        return _locationSystem ?? (_locationSystem = new LocationSystem());
    }*/

    private static void MasterEvent(MasterEventInfo me)
    {
        if (me.locationEventInfo != null)
        {
            if (me.dialogueEventInfo != null)
                if (me.dialogueEventInfo.eventType != DIALOGUE_EVENT_TYPE.EndDialogue)
                {
                    ShowImage(me.locationEventInfo);
                    return;
                }
            if(me.entityEventInfo != null)
                if (me.entityEventInfo.entityEventType != ENTITY_EVENT_TYPE.ENTITY_STOP)
                {
                    ShowImage(me.locationEventInfo);
                    return;
                }

            if (me.progressEventInfo && !me.progressEventInfo.done &&
                me.progressEventInfo.progressEventType == PROGRESS_EVENT_TYPE.ADD_TO_LOCATION) return;
            LocationEvent(me.locationEventInfo);
        }
    }

    private static void LocationEvent(LocationEventInfo locationEventInfo)
    {
        Game.currentLocation = locationEventInfo.location;
        ShowImage(locationEventInfo);
        AssignButtons(locationEventInfo);
    }

    private static void ShowImage(LocationEventInfo locationEventInfo)
    {
        locationPanelImage.sprite = locationEventInfo.location.image;
    }

    private static void AssignButtons(LocationEventInfo locationEventInfo)
    {
        locationEventInfo.location.EvaluateEventHolders();
        for (int i = 0; i < 4-locationEventInfo.location.eventHolders.Count; i++)
        {
            _buttonEventHandlers[i].SetEventInfo(null);
        }

        for (int i = 0; i < locationEventInfo.location.eventHolders.Count ; i++)
        {
            _buttonEventHandlers[3-i].SetEventInfo(locationEventInfo.location.eventHolders[i].GetEvent());
        }
    }
}