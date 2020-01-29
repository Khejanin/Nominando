using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class LocationSystem
{
    //I dont want this to be a singleton

    //private static LocationSystem _locationSystem;
    private static readonly GameObject locationPanel;
    private static readonly Image locationPanelImage;
    private static readonly GameObject LocationNamePanel;
    private static readonly TextMeshProUGUI locationName;

    private static LocationEventInfo currentLocationEventInfo;

    private static readonly ButtonEventHandler[] _buttonEventHandlers = new ButtonEventHandler[4];

    static LocationSystem()
    {
        locationPanel = GameObject.Find("LocationPanel");
        LocationNamePanel = GameObject.Find("LocationNamePanel");
        locationName = GameObject.Find("LocationName").GetComponentInChildren<TextMeshProUGUI>();
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
                    Show(me.locationEventInfo);
                    return;
                }
            if(me.entityEventInfo != null)
                if (me.entityEventInfo.entityEventType != ENTITY_EVENT_TYPE.ENTITY_STOP)
                {
                    Show(me.locationEventInfo);
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
        ProgressEventInfo e = ScriptableObject.CreateInstance<ProgressEventInfo>();
        e.progressIndex = locationEventInfo.location.progressIndex;
        e.progressEventType = PROGRESS_EVENT_TYPE.GAME_PROGRESS;
        EventSystem.EventSystem.FireEvent(e);
        Show(locationEventInfo);
        AssignButtons(locationEventInfo);
    }

    private static void Show(LocationEventInfo locationEventInfo)
    {
        locationName.text = locationEventInfo.location.name;
        UIHelperClass.ShowPanel(LocationNamePanel,true);
        if (locationEventInfo.location.Texture == null)
        {
            currentLocationEventInfo = locationEventInfo;
            APIHandler.getAPIHandler().FetchImage(locationEventInfo.location.imagePath, FetchImageCallback);
        }
        else
        {
            locationPanelImage.sprite = Sprite.Create(
                locationEventInfo.location.Texture, 
                new Rect(0,0,locationEventInfo.location.Texture.width, locationEventInfo.location.Texture.height),
                new Vector2(0.5f, 0.5f));
            
            locationPanelImage.SetMaterialDirty();
        }
    }

    public static void HideNamePanel()
    {
        UIHelperClass.ShowPanel(LocationNamePanel,false);
    }

    public static void FetchImageCallback(Texture2D texture)
    {
        currentLocationEventInfo.location.Texture = texture;
        Sprite tempSpr = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height),new Vector2(0.5f, 0.5f));
        locationPanelImage.sprite = tempSpr;
        locationPanelImage.SetMaterialDirty();
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