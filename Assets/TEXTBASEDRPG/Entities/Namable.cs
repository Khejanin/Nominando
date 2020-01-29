using System;
using System.Threading.Tasks;
using EventSystem;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Namable
{
    public abstract class Namable : ScriptableObject , IEventHolder
    {
        public string uniqueID;
        public new string name;
        public Texture2D Texture;
        public string imagePath;
        public Dialogue namableDialogue;
        public Dialogue imageDialogue;

        public bool juicy = false;

        [HideInInspector] public int imageHeight = 256;
        [HideInInspector] public int imageWidth = 256;
        public static string saveLocationPath = "Assets/Resources/img/";

        public NAMABLE_STATE namableState = NAMABLE_STATE.INITIAL;

        public bool canBeReset = true;

        public enum NAMABLE_STATE
        {
            INITIAL,
            NAMABLE_DIALOGUE_FINISH,
            NAMED,
            IMAGE_DIALOGUE_FINISH,
            COMPLETE
        }

        public ImageCropper.Settings GetImageSettings()
        {
            ImageCropper.Settings result = new ImageCropper.Settings();
            result.markTextureNonReadable = false;
            result.selectionMinAspectRatio = result.selectionMaxAspectRatio = (float)imageWidth / imageHeight;
            return result;
        }
        
        public virtual void Clear()
        {
            name = "";
            namableState = NAMABLE_STATE.INITIAL;
            imagePath = "";
        }

        private void OnEnable()
        {
        }

        public void SetTexture(Texture2D texture)
        {
            this.Texture = texture;
            if (!name.Equals("") && Texture != null)
            {
                namableState = NAMABLE_STATE.COMPLETE;
            }
        }

        public void SetName(string nam)
        {
            name = nam;
            if (!name.Equals("") && Texture != null)
            {
                namableState = NAMABLE_STATE.COMPLETE;
                string json = JsonUtility.ToJson(this ,true);
                Debug.Log(json);
            }
        }

        public virtual EventInfo GetEvent()
        {
            throw new System.NotImplementedException();
        }

        public abstract APIHandler.EntityUploadJSON getUploadData();

        public virtual void SetData(string nameData, NAMABLE_STATE stateData)
        {
            name = nameData;
            namableState = stateData;
        }
    }
}