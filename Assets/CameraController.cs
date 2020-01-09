using System;
using System.Runtime.CompilerServices;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private WebCamTexture _mCamera;
    private GameObject _cameraPanel;
    private CameraResult _callbackCameraResult;
    private NamableEventInfo _eventInfo;

    // Use this for initialization
    private void Start()
    {
        
    }

    public void Activate(CameraResult result,NamableEventInfo info)
    {
        _eventInfo = info;
        _cameraPanel = gameObject;
        if (!_mCamera)
        {
            _mCamera = new WebCamTexture();
            _cameraPanel.GetComponent<Image>().material.mainTexture = _mCamera;
        }
        _callbackCameraResult = result;
        ShowPanel(true);
    }

    public delegate void CameraResult(Sprite sprite);
    
    // Update is called once per frame
    private void Update()
    {
        if ((Input.GetKeyDown("space") || 
             Input.touchCount > 0 && 
             Input.GetTouch(0).phase == TouchPhase.Began))
        {
            var texture = _cameraPanel.GetComponent<Image>().material.mainTexture as WebCamTexture;
            var settings = _eventInfo.namable.imageSettings;
            var texture2D = new Texture2D(texture.width, texture.height);

            texture2D.SetPixels(texture.GetPixels());
            texture2D.Apply();
            
            ShowPanel(false);
            
            ImageCropper.Instance.Show(texture2D, CropResult, settings, ImageResizePolicy);
        }
    }
    
    public void CropResult(bool result, Texture Original, Texture2D croppedImage)
    {
        if (result)
        {
            Debug.Log(croppedImage.width);
            Debug.Log(croppedImage.height);
            Sprite sprite = Sprite.Create(croppedImage,
                new Rect(0, 0, croppedImage.width, croppedImage.height), new Vector2(0.5f, 0.5f));
            _callbackCameraResult.Invoke(sprite);
        }
        else
        {
            ShowPanel(true);
        }
    }

    public void ImageResizePolicy(ref int width, ref int height)
    {
        width = _eventInfo.namable.imageWidth;
        height = _eventInfo.namable.imageHeight;
    }

    private void ShowPanel(bool show)
    {
        if(show) _mCamera.Play();
        else _mCamera.Stop();
        _cameraPanel.SetActive(show);
        UIHelperClass.ShowPanel(_cameraPanel,show);
        enabled = show;
    }
}