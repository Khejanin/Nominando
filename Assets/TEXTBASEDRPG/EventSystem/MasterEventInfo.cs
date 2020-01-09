using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(fileName = "New MasterEventInfo", menuName = "EventSystem/MasterEventInfo")]
    public class MasterEventInfo : EventInfo
    {
        public DialogueEventInfo dialogueEventInfo;
        public EntityEventInfo entityEventInfo;
        public LocationEventInfo locationEventInfo;
        public ProgressEventInfo progressEventInfo;
    }
}