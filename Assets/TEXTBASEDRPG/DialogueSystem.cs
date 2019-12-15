using System.Collections;
using System.Collections.Generic;
using TEXTBASEDRPG.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{

    public List<Button> buttons = new List<Button>();
    public List<MasterEvent> _events = new List<MasterEvent>();
    private List<TextMeshProUGUI> _buttonTexts = new List<TextMeshProUGUI>();

    private MasterEvent defaultContinueEvent;
    private EventHandler _eventHandler;

    public TextMeshProUGUI dialogueTextMesh;

    Dialogue _currentDialogue;

    public Dialogue getCurrentDialogue()
    {
        return _currentDialogue;
    }

    public void SetOptions(DialogueOptions options)
    {
        for (int i = 0; i < _buttonTexts.Count; i++)
        {
            _buttonTexts[i].SetText(options.options[i].optionString);
            _events[i] = options.options[i].masterEvent;
            buttons[i].enabled = true;
        }
    }

    public void setLinearOptions()
    {
        for (int i = 0; i < _buttonTexts.Count; i++)
        {
            _buttonTexts[i].SetText("");
            buttons[i].enabled = false;
        }

        buttons[3].enabled = true;
        _events[3] = defaultContinueEvent;
        _buttonTexts[3].SetText("Continue");
    }

    private void Awake()
    {
        _eventHandler = EventHandler.getEventHandler();
        bool done = false;
        for (var i = 0; !done; i++)
        {
            _buttonTexts.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());
            _events.Add(null);
            buttons[i].enabled = false;
            buttons[i].onClick.AddListener(() =>
            {
                Debug.Log(i);
                EventHandler.getEventHandler().HandleEvent(_events[i]);
            });

            if (i == 3) done = true;
        }

        defaultContinueEvent = new MasterEvent { interaction = MasterEvent.InteractionType.Talk, 
                                                    autosave = false,
                                                    doOnce = false,
                                                    e = new MasterEvent.DialogueContinueEvent(),
                                                    isDone = false};
    }
    
    public void doUpdate()
    {
        //updateDialogue();
    }

    private void updateDialogue()
    {
        dialogueTextMesh.SetText(_currentDialogue.getDialogue());
    }


    public void setCurrentDialogue(Dialogue d)
    {
        _currentDialogue = d;
        checkAndSetOptions();
        updateDialogue();
    }

    public void continueDialogue(DialogueOption option)
    {
        _currentDialogue.continueDialogue(option);
        checkAndSetOptions(); //Uodates the options
        updateDialogue(); //Updates the Text
    }

    private void checkAndSetOptions()
    {
        Debug.Log("yes!");
        if (_currentDialogue.current is DialogueNodeSplit)
        {
            DialogueNodeSplit split = (DialogueNodeSplit)_currentDialogue.current;
            SetOptions(split.options);
        }
        else
        {
            setLinearOptions();
        }
    }
}
