using System.Collections;
using System.Collections.Generic;
using TEXTBASEDRPG.EventSystem;
using UnityEngine;

[System.Serializable]
public class DialogueOption : MonoBehaviour
{

    public string optionString;

    public DialogueNode next;

    public EventSystem eventSystem;

}
