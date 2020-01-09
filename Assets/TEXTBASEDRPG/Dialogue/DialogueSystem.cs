using BrunoMikoski.TextJuicer;
using EventSystem;
using TMPro;
using UnityEngine;

public static class DialogueSystem
{
    //private static DialogueSystem _dialogueSystem;

    private static readonly ButtonEventHandler[] _buttonEventHandlers = new ButtonEventHandler[4];

    private static DialogueEventInfo _defaultContinueEventInfo;

    private static GameObject _dialoguePanel;
    private static TextMeshProUGUI _dialogueTextMesh;
    private static TMP_TextJuicer _textJuicer;

    private static Dialogue _currentDialogue;

    static DialogueSystem()
    {
        Instantiate();
        EventSystem.EventSystem.RegisterListener<MasterEventInfo>(MasterEvent);
        EventSystem.EventSystem.RegisterListener<DialogueEventInfo>(DialogueEvent);
    }

    public static void Start() {}

    private static void MasterEvent(MasterEventInfo me)
    {
        if (me.dialogueEventInfo != null) DialogueEvent(me.dialogueEventInfo);
    }

    private static void DialogueEvent(DialogueEventInfo e)
    {
        Debug.Log(e);
        switch (e.eventType)
        {
            case DIALOGUE_EVENT_TYPE.StartDialogue:
                UIHelperClass.ShowPanel(_dialoguePanel, true);
                SetCurrentDialogue(e.dialogue);
                break;
            case DIALOGUE_EVENT_TYPE.ContinueDialogue:
                ContinueDialogue(e.node);
                break;
            case DIALOGUE_EVENT_TYPE.EndDialogue:
                UIHelperClass.ShowPanel(_dialoguePanel, false);
                break;
        }
    }

    /*public static DialogueSystem GetDialogueSystem()
    {
        return _dialogueSystem ?? (_dialogueSystem = new DialogueSystem()); //If null, right, if not, left, simple!
    }*/

    private static void Instantiate()
    {
        _dialoguePanel = GameObject.Find("DialoguePanel").gameObject;
        _textJuicer = Object.FindObjectOfType<TMP_TextJuicer>().gameObject.GetComponent<TMP_TextJuicer>();
        UIHelperClass.ShowPanel(_dialoguePanel, false);

        for (var i = 0; i < 4; i++)
            _buttonEventHandlers[i] = GameObject.Find("Option" + i).GetComponent<ButtonEventHandler>();

        _dialogueTextMesh = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();

        _defaultContinueEventInfo = ScriptableObject.CreateInstance<DialogueEventInfo>();
        _defaultContinueEventInfo.eventType = DIALOGUE_EVENT_TYPE.ContinueDialogue;
        _defaultContinueEventInfo.Validate();
    }

    public static Dialogue GetCurrentDialogue()
    {
        return _currentDialogue;
    }

    private static void SetOptions(EventInfo[] options)
    {
        for (int i = 0; i < 4-options.Length; i++)
        {
            _buttonEventHandlers[i].SetEventInfo(null);
        }

        for (int i = 0; i < options.Length ; i++)
        {
            _buttonEventHandlers[3-i].SetEventInfo(options[i]);
        }
    }

    private static void SetLinearOptions()
    {
        for (var i = 0; i < _buttonEventHandlers.Length; i++) _buttonEventHandlers[i].SetEventInfo(null);

        _buttonEventHandlers[3].SetEventInfo(_defaultContinueEventInfo);
    }

    private static void UpdateDialogueTextMesh()
    {
        _dialogueTextMesh.SetText(_currentDialogue.GetDialogue());
        _textJuicer.SetDirty();
    }

    private static void SetCurrentDialogue(Dialogue d)
    {
        _currentDialogue = d;
        DoUpdate();
    }

    private static void ContinueDialogue(DialogueNode next)
    {
        if(_currentDialogue.ContinueDialogue(next))
            DoUpdate();
    }

    private static void DoUpdate()
    {
        CheckAndSetOptions();
        UpdateDialogueTextMesh();
    }

    private static void CheckAndSetOptions()
    {
        if (_currentDialogue.current is DialogueNodeSplit)
        {
            var split = (DialogueNodeSplit) _currentDialogue.current;
            SetOptions(split.options.ToArray());
        }
        else
        {
            SetLinearOptions();
        }
    }
}