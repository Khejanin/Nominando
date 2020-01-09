using System;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using Namable;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public ButtonEventHandler[] actionButtons = new ButtonEventHandler[4];
    //private LocationSystem _locationSystem;
    //private EntitySystem _entitySystem;

    public GameObject cameraPanel;

    public static Location currentLocation;

    public bool cameraTestMode;

    private bool inputEnabled = true;
    public GameObject LocationPanel;

    public List<Location> objectos;

    public Dialogue startDialogue;

    public MasterEventInfo startMasterEvent;

    // Start is called before the first frame update
    public void StartGame()
    {
        //TouchScreenKeyboard.Open("test", TouchScreenKeyboardType.Default, false, false, false, false, "", 32);
        //Debug.Log(TouchScreenKeyboard.isSupported);
        if (!cameraTestMode)
        {
            //_dialogueSystem = DialogueSystem.GetDialogueSystem();
            //_locationSystem = LocationSystem.GetLocationSystem();
            //_entitySystem = EntitySystem.GetEntitySystem();
            LocationSystem.Start();
            DialogueSystem.Start();
            EntitySystem.Start();
            NameableSystem.Start();
            ProgressSystem.Start();
            
            if (startMasterEvent != null) EventSystem.EventSystem.FireEvent(startMasterEvent);
        }
        else
        {
            var mCamera = new WebCamTexture();
            cameraPanel.GetComponent<Image>().material.mainTexture = mCamera;
            mCamera.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (cameraTestMode)
            if (inputEnabled && (Input.GetKeyDown("space") ||
                                 Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (cameraPanel.activeSelf)
                {
                    inputEnabled = false;
                    var texture = cameraPanel.GetComponent<Image>().material.mainTexture as WebCamTexture;

                    var settings = new ImageCropper.Settings();
                    var texture2D = new Texture2D(texture.width, texture.height);

                    settings.selectionMinAspectRatio = settings.selectionMaxAspectRatio = 1;

                    texture2D.SetPixels(texture.GetPixels());
                    texture2D.Apply();
                    //ImageCropper.ImageResizePolicy policy = new ImageCropper.ImageResizePolicy(TargetWidth,targetHeight);
                    ImageCropper.Instance.Show(texture2D, cropResult, settings, ImageResizePolicy);


                    if (false)
                    {
                        Debug.Log(texture.GetType());


                        Debug.Log(texture.width + "  " + texture.height);

                        cameraPanel.GetComponent<RectTransform>().rect.Set(0, 0, texture.width, texture.height);

                        /*
                        */

                        //IntPtr pointer = texture.GetNativeTexturePtr();
                        //texture2D.UpdateExternalTexture(pointer);

                        texture2D.SetPixels(texture.GetPixels());
                        texture2D.Apply();
                    }

                    //LocationPanel.GetComponent<Image>().sprite = Sprite.Create(texture2D,new Rect(0,0,texture2D.width,texture2D.height),new Vector2(0.5f,0.5f));
                }

                Debug.Log("Space pressed");
                cameraPanel.SetActive(!cameraPanel.activeSelf);
            }
    }

    public void cropResult(bool result, Texture Original, Texture2D croppedImage)
    {
        if (result)
        {
            Debug.Log(croppedImage.width);
            Debug.Log(croppedImage.height);
            LocationPanel.GetComponent<Image>().sprite = Sprite.Create(croppedImage,
                new Rect(0, 0, croppedImage.width, croppedImage.height), new Vector2(0.5f, 0.5f));
            inputEnabled = true;
        }
        else
        {
            inputEnabled = true;
            cameraPanel.SetActive(!cameraPanel.activeSelf);
        }
    }

    public void ImageResizePolicy(ref int width, ref int height)
    {
        width = 256;
        height = 256;
    }
}