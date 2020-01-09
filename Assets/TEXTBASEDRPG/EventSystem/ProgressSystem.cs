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
        if(me.progressEventInfo != null) ProgressEvent(me.progressEventInfo);
    }

    private static void ProgressEvent(ProgressEventInfo progressEventInfo)
    {
        if (!progressEventInfo.done)
        {
            progressEventInfo.done = true;
            if (progressEventInfo.progressEventType == PROGRESS_EVENT_TYPE.ADD_TO_LOCATION)
            {
                if (progressEventInfo.eventHolderToAddOrRemove != null)
                    if(progressEventInfo.eventHolderToAddOrRemove.GetType() == typeof(Entity))
                        progressEventInfo.location.entities.Add(progressEventInfo.eventHolderToAddOrRemove as Entity);
                    else if(progressEventInfo.eventHolderToAddOrRemove.GetType() == typeof(Location))
                        progressEventInfo.location.locations.Add(progressEventInfo.eventHolderToAddOrRemove as Location);
                    else if(progressEventInfo.eventHolderToAddOrRemove.GetType() == typeof(Action))
                        progressEventInfo.location.actions.Add(progressEventInfo.eventHolderToAddOrRemove as Action);
                EventSystem.EventSystem.FireEvent(progressEventInfo.location.GetEvent());
            }
            else if(progressEventInfo.progressEventType == PROGRESS_EVENT_TYPE.REMOVE_FROM_LOCATION)
            {
                if (progressEventInfo.eventHolderToAddOrRemove != null)
                    if(progressEventInfo.eventHolderToAddOrRemove.GetType() == typeof(Entity))
                        progressEventInfo.location.entities.Remove(progressEventInfo.eventHolderToAddOrRemove as Entity);
                    else if(progressEventInfo.eventHolderToAddOrRemove.GetType() == typeof(Location))
                        progressEventInfo.location.locations.Remove(progressEventInfo.eventHolderToAddOrRemove as Location);
                    else if(progressEventInfo.eventHolderToAddOrRemove.GetType() == typeof(Action))
                        progressEventInfo.location.actions.Remove(progressEventInfo.eventHolderToAddOrRemove as Action);
            }
        }
    }

  
}