using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public DialogueSystem dialogueSystem;

    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        TouchScreenKeyboard.Open("test", TouchScreenKeyboardType.Default, false, false, false, false, "", 32);
        Debug.Log(TouchScreenKeyboard.isSupported);
        dialogueSystem.setCurrentDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
