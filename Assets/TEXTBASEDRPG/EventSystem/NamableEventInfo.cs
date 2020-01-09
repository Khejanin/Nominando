using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New NamableEventInfo", menuName = "EventSystem/NamableEventInfo")]
    public class NamableEventInfo : EventInfo
    {
        public NAMABLE_EVENT_STATE eventState;
        public Namable.Namable namable;
    }


    public enum NAMABLE_EVENT_STATE
    {
        NAME_ONLY,
        PICTURE_ONLY,
        NAME_AND_PICTURE
    }
}