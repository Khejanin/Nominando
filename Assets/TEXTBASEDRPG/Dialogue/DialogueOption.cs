using System.Collections;
using System.Collections.Generic;
using TEXTBASEDRPG.EventSystem;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class DialogueOption : MonoBehaviour
{

    public string optionString;

    public DialogueNode next;

    [FormerlySerializedAs("masterEventMono")] [FormerlySerializedAs("eventSystem")] public MasterEvent masterEvent;

}
