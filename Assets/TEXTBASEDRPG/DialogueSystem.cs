using System.Collections;
using System.Collections.Generic;
using TEXTBASEDRPG.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{

    public List<Button> buttons;
    public List<EventSystem> _events;
    private List<TextMeshProUGUI> _buttonTexts;

    public TextMeshProUGUI dialogueTextMesh;

    Dialogue _currentDialogue;

    public void SetOptions(DialogueOptions options)
    {
        //@TODO
    }

    public void setLinearOptions()
    {
        /*optionButton1.updateOption(null);
        optionButton1.updateText("Continue");
        optionButton2.updateOption(null);
        optionButton2.updateText("");
        optionButton3.updateOption(null);
        optionButton3.updateText("");
        optionButton4.updateOption(null);
        optionButton4.updateText("");*/
    }

    private void Start()
    {
        for (var i = 0; i < _buttonTexts.Count; i++)
        {
            _buttonTexts[i] = buttons[i].GetComponent<TextMeshProUGUI>();
            buttons[i].onClick.AddListener((i) =>
            {
                
            });
        }
        
    }

    void fetchButtonInfo1()
    {
        _currentDialogue.continueDialogue(option1.GetComponent<OptionButton>().getOption());
        continueDialogue();
    }

    void fetchButtonInfo2()
    {
        _currentDialogue.continueDialogue(option2.GetComponent<OptionButton>().getOption());
        continueDialogue();
    }

    void fetchButtonInfo3()
    {
        _currentDialogue.continueDialogue(option3.GetComponent<OptionButton>().getOption());
        continueDialogue();
    }

    void fetchButtonInfo4()
    {
        _currentDialogue.continueDialogue(option4.GetComponent<OptionButton>().getOption());
        continueDialogue();
    }

    public void doUpdate()
    {
        //updateDialogue();
    }

    public void updateDialogue()
    {
        dialogueTextMesh.SetText(_currentDialogue.getDialogue());
    }


    public void setCurrentDialogue(Dialogue d)
    {
        _currentDialogue = d;
        checkAndSetOptions();
        updateDialogue();
    }

    public void continueDialogue()
    {
        checkAndSetOptions();
        updateDialogue();
    }

    public void checkAndSetOptions()
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
