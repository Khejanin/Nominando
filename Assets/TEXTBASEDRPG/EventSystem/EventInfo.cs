using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New EventInfo", menuName = "EventSystem/EventInfo")]
    public abstract class EventInfo : ScriptableObject
    {
        public string buttonOptionString;
        public string EventInfoString;
    }
}