using Namable;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New LocationEventInfo", menuName = "EventSystem/LocationEventInfo")]
    public class LocationEventInfo : EventInfo
    {
        public Location location;
    }
}