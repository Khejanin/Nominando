using System.Collections;
using System.Collections.Generic;
using Namable;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New ProgressEventInfo", menuName = "EventSystem/ProgressEventInfo")]
    public class ProgressEventInfo : EventInfo
    {
        public Location location;
        public IEventHolder eventHolder;

        public IEventHolder eventHolderToAddOrRemove;

        public bool done;

        public PROGRESS_EVENT_TYPE progressEventType;
    }

    public enum PROGRESS_EVENT_TYPE
    {
        ADD_TO_LOCATION,
        REMOVE_FROM_LOCATION,
        ADD_TO_INVENTORY
    }
}