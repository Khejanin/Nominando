using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using EventSystem;
using Namable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class NameableSystem
{
    private static readonly GameObject _cameraPanel;
    private static readonly GameObject _nameableInput;
    private static readonly TMP_InputField _inputField;
    private static readonly Button _inputSubmitButton;
    private static readonly CameraController _cameraController;
    private static NamableEventInfo _eventInfo;
    
    private static GameObject _locationPanel;
    
    static NameableSystem()
    {
        _locationPanel = GameObject.Find("LocationPanel");
        _cameraPanel = GameObject.Find("CameraPanel");
        _cameraController = _cameraPanel.GetComponent<CameraController>();
        _nameableInput = GameObject.Find("NameableInput");
        _inputField = _nameableInput.GetComponentInChildren<TMP_InputField>();
        _inputSubmitButton = _nameableInput.GetComponentInChildren<Button>();
        _inputSubmitButton.onClick.AddListener(SubmitEvent);
        _cameraPanel.SetActive(false);
        _nameableInput.SetActive(false);
        UIHelperClass.ShowPanel(_cameraPanel,true);
        UIHelperClass.ShowPanel(_nameableInput,true);
        //_inputField.onSubmit.AddListener(SubmitEvent);
        //_inputField.onDeselect.AddListener(SubmitEvent);
        //_inputField.onEndEdit.AddListener(SubmitEvent);
        //EventSystem.EventSystem.RegisterListener<MasterEventInfo>(MasterEvent);
        EventSystem.EventSystem.RegisterListener<NamableEventInfo>(NameableEvent);
    }

    /*private static void MasterEvent(MasterEventInfo me)
    {
        if (me.locationEventInfo != null)
        {
            if (me.dialogueEventInfo != null || me.entityEventInfo != null)
            {
                showImage(me.locationEventInfo);
            }
            else LocationEvent(me.locationEventInfo);
        }
    }*/

    public static void Start()
    {
    }

    private static void NameableEvent(NamableEventInfo nameableEventInfo)
    {
        switch (nameableEventInfo.eventState)
        {
            case NAMABLE_EVENT_STATE.NAME_ONLY:
                NameEvent(nameableEventInfo, null);
                break;
            case NAMABLE_EVENT_STATE.PICTURE_ONLY:
                PictureEvent(nameableEventInfo);
                break;
            case NAMABLE_EVENT_STATE.NAME_AND_PICTURE:
                NameEvent(nameableEventInfo, PictureEvent);
                break;
        }
    }

    private static async void NameEvent(NamableEventInfo nameableEventInfo, Action<NamableEventInfo> func)
    {
        _eventInfo = nameableEventInfo;
        ActionPanelScript.enableButtons(false);
        _nameableInput.SetActive(true);
        _inputField.text = "";
    }
    
    private static async Task<string> Keyboard(TouchScreenKeyboard keyboard)
    {
        while (keyboard.status == TouchScreenKeyboard.Status.Visible) await Task.Delay(100);
        return keyboard.text;
    }

    private static void PictureEvent(NamableEventInfo nameableEventInfo)
    {
        _eventInfo = nameableEventInfo;
        _cameraController.Activate(CameraResult,_eventInfo);
    }

    private static void SubmitEvent()
    {
        if (_inputField.text != "")
        {
            _eventInfo.namable.namableState = Namable.Namable.NAMABLE_STATE.NAMED;
            _eventInfo.namable.name = _inputField.text;
            EventSystem.EventSystem.FireEvent(_eventInfo.namable.GetEvent());
            _nameableInput.SetActive(false);
            _eventInfo.namable.SetName(_inputField.text);
        }
    }

    private static void CameraResult(Sprite sprite)
    {
        _eventInfo.namable.SetImage(sprite);
        EventSystem.EventSystem.FireEvent(_eventInfo.namable.GetEvent());
        //_locationPanel.GetComponent<Image>().sprite = sprite;
    }
}