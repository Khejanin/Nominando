using System;
using EventSystem;
using Namable;
using UnityEngine;
using UnityEngine.UI;

public static class ProgressSystem
{

    static ProgressSystem()
    {
        EventSystem.EventSystem.RegisterListener<MasterEventInfo>(MasterEvent);
        EventSystem.EventSystem.RegisterListener<ProgressEventInfo>(ProgressEvent);
    }

    public static void Start()
    {
    }

    private static void MasterEvent(MasterEventInfo me)
    {
        if (me.progressEventInfo != null) ProgressEvent(me.progressEventInfo);
    }

    private static void ProgressEvent(ProgressEventInfo progressEventInfo)
    {
        if (!progressEventInfo.done)
        {
            progressEventInfo.done = true;
            switch (progressEventInfo.progressEventType)
            {
                case PROGRESS_EVENT_TYPE.ADD_TO_LOCATION:
                    switch (progressEventInfo.AddRemoveType)
                    {
                        case ADD_REMOVE_TYPE.ACTION:
                            progressEventInfo.location.hideActionsNr--;
                            break;
                        case ADD_REMOVE_TYPE.ENTITY:
                            progressEventInfo.location.hideEntitiesNr--;
                            break;
                        case ADD_REMOVE_TYPE.LOCATION:
                            progressEventInfo.location.hideLocationsNr--;
                            break;
                    }
                    APIHandler.getAPIHandler().UploadEntityData(progressEventInfo.location.getUploadData());
                    break;
                case PROGRESS_EVENT_TYPE.REMOVE_FROM_LOCATION:
                    switch (progressEventInfo.AddRemoveType)
                    {
                        case ADD_REMOVE_TYPE.ACTION:
                            progressEventInfo.location.hideActionsNr++;
                            break;
                        case ADD_REMOVE_TYPE.ENTITY:
                            progressEventInfo.location.hideEntitiesNr++;
                            break;
                        case ADD_REMOVE_TYPE.LOCATION:
                            progressEventInfo.location.hideLocationsNr++;
                            break;
                    }
                    APIHandler.getAPIHandler().UploadEntityData(progressEventInfo.location.getUploadData());
                    break;
                case PROGRESS_EVENT_TYPE.ADD_TO_INVENTORY:
                    break;
                case PROGRESS_EVENT_TYPE.PROGRESS_DIALOGUE:
                    progressEventInfo.entity.dialogueState++;
                    APIHandler.getAPIHandler().UploadEntityData(progressEventInfo.entity.getUploadData());
                    break;
                case PROGRESS_EVENT_TYPE.PROGRESS_OPTIONS:
                    progressEventInfo.options.hideOptions--;
                    break;
            }
        }
    }
}