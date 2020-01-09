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
        public Sprite image;
        public Dialogue namableDialogue;
        public Dialogue imageDialogue;

        [HideInInspector]
        public int imageHeight;
        [HideInInspector]
        public int imageWidth;
        [NotNull] public ImageCropper.Settings imageSettings;
        public static string saveLocationPath = "Assets/Resources/img/";

        public NAMABLE_STATE namableState = NAMABLE_STATE.INITIAL;

        public enum NAMABLE_STATE
        {
            INITIAL,
            NAMABLE_DIALOGUE_FINISH,
            NAMED,
            IMAGE_DIALOGUE_FINISH,
            COMPLETE
        }

        public void InitImageSettings(int imageWidth,int imageHeight)
        {
            imageSettings = new ImageCropper.Settings();
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            imageSettings.selectionMinAspectRatio = imageSettings.selectionMaxAspectRatio = (float)imageWidth / imageHeight;
        }

        private void OnEnable()
        {
            InitImageSettings(256,256);
        }

        public void SetImage(Sprite sprite)
        {
            image = sprite;
            if (!name.Equals("") && image != null)
            {
                namableState = NAMABLE_STATE.COMPLETE;
            }
        }

        public void SetName(string nam)
        {
            name = nam;
            if (!name.Equals("") && image != null)
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

        public virtual void LoadData()
        {
            
        }

        public virtual void SetData(string nameData, NAMABLE_STATE stateData)
        {
            name = nameData;
            namableState = stateData;
        }
    }
}