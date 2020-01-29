using System;
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
        public Entity entity;
        public DialogueNodeSplit options;
        public IEventHolder eventHolder;

        public int progressIndex;

        public IEventHolder eventHolderToAddOrRemove;

        public bool done;

        public PROGRESS_EVENT_TYPE progressEventType;
        public ADD_REMOVE_TYPE AddRemoveType;

        private void Awake()
        {
            done = false;
        }

        private void OnValidate()
        {
            done = false;
        }
    }

    public enum PROGRESS_EVENT_TYPE
    {
        ADD_TO_LOCATION,
        REMOVE_FROM_LOCATION,
        ADD_TO_INVENTORY,
        PROGRESS_DIALOGUE,
        PROGRESS_OPTIONS,
        GAME_PROGRESS
    }

    public enum ADD_REMOVE_TYPE
    {
        ENTITY,
        ACTION,
        LOCATION
    }
}