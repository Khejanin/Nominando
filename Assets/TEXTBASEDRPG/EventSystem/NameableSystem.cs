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
    private static GameObject _cameraPanel;
    private static GameObject _nameableInput;
    private static TMP_InputField _inputField;
    private static Button _inputSubmitButton;
    private static CameraController _cameraController;
    private static NamableEventInfo _eventInfo;
    
    private static GameObject _locationPanel;

    public static void SetElements(GameObject locationPanel, GameObject cameraPanel, GameObject nameableInput)
    {
        _locationPanel = locationPanel;
        _cameraPanel = cameraPanel;
        _nameableInput = nameableInput;
        _cameraController = _cameraPanel.GetComponent<CameraController>();
        _inputField = _nameableInput.GetComponentInChildren<TMP_InputField>();
        _inputSubmitButton = _nameableInput.GetComponentInChildren<Button>();
        _inputSubmitButton.onClick.AddListener(SubmitEvent);
        _cameraPanel.SetActive(false);
        _nameableInput.SetActive(false);
        UIHelperClass.ShowPanel(_cameraPanel,true);
        UIHelperClass.ShowPanel(_nameableInput,true);
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
            SendAPIUploadRequest();
            _eventInfo.namable.name = _inputField.text;
            EventSystem.EventSystem.FireEvent(_eventInfo.namable.GetEvent());
            _nameableInput.SetActive(false);
            _eventInfo.namable.SetName(_inputField.text);
        }
    }

    private static void SendAPIUploadRequest()
    {
        APIHandler.EntityUploadJSON uploadData = new APIHandler.EntityUploadJSON();
        uploadData.isNamable = (_eventInfo.namable.GetType() != typeof(Location));
        if (!uploadData.isNamable)
        {
            uploadData.HideActionNr = (_eventInfo.namable as Location).hideActionsNr;
            uploadData.HideEntityNr = (_eventInfo.namable as Location).hideEntitiesNr;
            uploadData.HideLocationNr = (_eventInfo.namable as Location).hideLocationsNr;
        }
        uploadData.Name = _inputField.text;
        uploadData.UniqueId = _eventInfo.namable.uniqueID;
        uploadData.State = _eventInfo.namable.namableState.ToString();
        APIHandler.getAPIHandler().UploadEntityData(uploadData);
    }

    private static void CameraResult(Texture2D tex)
    {
        _eventInfo.namable.SetTexture(tex);
        SendAPIUploadImageRequest(_eventInfo.namable,tex);
        _eventInfo.namable.namableState = Namable.Namable.NAMABLE_STATE.COMPLETE;
        SendAPIUploadRequest();
        EventSystem.EventSystem.FireEvent(_eventInfo.namable.GetEvent());
    }

    private static void SendAPIUploadImageRequest(Namable.Namable nam,Texture2D tex)
    {
        APIHandler.getAPIHandler().UploadEntityImage(nam, tex);
    }
}