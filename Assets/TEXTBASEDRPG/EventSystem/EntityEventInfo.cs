using Namable;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New EntityEventInfo", menuName = "EventSystem/EntityEventInfo")]
    public class EntityEventInfo : EventInfo
    {
        public Entity entity;
        public ENTITY_EVENT_TYPE entityEventType;
    }

    public enum ENTITY_EVENT_TYPE
    {
        ENTITY_START,
        ENTITY_STOP
    }
}