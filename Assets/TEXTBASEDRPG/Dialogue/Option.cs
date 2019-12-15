using System.Collections;
using System.Collections.Generic;
using TEXTBASEDRPG.EventSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class Option : MonoBehaviour
{
    public string optionString;

    [FormerlySerializedAs("masterEventMono")] [FormerlySerializedAs("eventSystem")] public MasterEvent masterEvent;


}
