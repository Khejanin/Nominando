using System.Collections.Generic;
using BrunoMikoski.TextJuicer.Modifiers;
using TMPro;
using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
    [ExecuteInEditMode]
    [AddComponentMenu("UI/Text Juicer")]
    public sealed class TMP_TextJuicer : MonoBehaviour
    {
        [SerializeField] private bool animationControlled;

        private TMP_MeshInfo[] cachedMeshInfo;
        private string cachedText = string.Empty;

        private CharacterData[] charactersData;

        [SerializeField] private float delay = 0.05f;

        private bool dispatchedAfterReadyMethod;

        [SerializeField] private float duration = 0.1f;

        private bool forceUpdate;

        private float internalTime;
        private bool isDirty = true;

        [SerializeField] private bool loop;

        [SerializeField] private bool playForever;

        [SerializeField] private bool playWhenReady = true;

        private float realTotalAnimationTime;

        private RectTransform rectTransform;
        private TMP_TextInfo textInfo;

        [SerializeField] private TMP_Text tmpText;

        private bool updateGeometry;
        private bool updateVertexData;
        private TextJuicerVertexModifier[] vertexModifiers;

        private TMP_Text TmpText
        {
            get
            {
                if (tmpText == null)
                {
                    tmpText = GetComponent<TMP_Text>();
                    if (tmpText == null)
                        tmpText = GetComponentInChildren<TMP_Text>();
                }

                return tmpText;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = (RectTransform) transform;
                return rectTransform;
            }
        }

        public string Text
        {
            get => TmpText.text;
            set
            {
                TmpText.text = value;
                SetDirty();
                UpdateIfDirty();
            }
        }

        [field: SerializeField]
        [field: Range(0.0f, 1.0f)]
        public float Progress { get; private set; }

        public bool IsPlaying { get; private set; }

        #region Unity Methods

        private void OnValidate()
        {
            cachedText = string.Empty;
            SetDirty();

            if (tmpText == null)
            {
                tmpText = GetComponent<TMP_Text>();
                if (tmpText == null)
                    tmpText = GetComponentInChildren<TMP_Text>();
            }
        }

        private void Awake()
        {
            if (!animationControlled && Application.isPlaying)
                SetProgress(0);
        }

        private void OnDisable()
        {
            forceUpdate = true;
        }

        public void Update()
        {
            if (!IsAllComponentsReady())
                return;

            UpdateIfDirty();

            if (!dispatchedAfterReadyMethod)
            {
                AfterIsReady();
                dispatchedAfterReadyMethod = true;
            }

            CheckProgress();
            UpdateTime();
            if (IsPlaying || animationControlled || forceUpdate)
                ApplyModifiers();
        }

        #endregion

        #region Interaction Methods

        public void Restart()
        {
            internalTime = 0;
        }

        public void Play()
        {
            Play(true);
        }

        public void Play(bool fromBeginning = true)
        {
            if (!IsAllComponentsReady())
            {
                playWhenReady = true;
                return;
            }

            if (fromBeginning)
                Restart();

            IsPlaying = true;
        }

        public void Complete()
        {
            if (IsPlaying)
                Progress = 1.0f;
        }

        public void Stop()
        {
            IsPlaying = false;
        }

        public void SetProgress(float targetProgress)
        {
            Progress = targetProgress;
            internalTime = Progress * realTotalAnimationTime;
            UpdateTime();
            ApplyModifiers();
            tmpText.havePropertiesChanged = true;
        }

        public void SetPlayForever(bool shouldPlayForever)
        {
            playForever = shouldPlayForever;
        }

        public CustomYieldInstruction WaitForCompletion()
        {
            return new TextJuicer_WaitForCompletion(this);
        }

        #endregion

        #region Internal

        private void AfterIsReady()
        {
            if (!Application.isPlaying)
                return;

            if (playWhenReady)
                Play();
            else
                SetProgress(Progress);
        }

        private bool IsAllComponentsReady()
        {
            if (TmpText == null)
                return false;

            if (TmpText.textInfo == null)
                return false;

            if (TmpText.mesh == null)
                return false;

            if (TmpText.textInfo.meshInfo == null)
                return false;
            return true;
        }


        private void ApplyModifiers()
        {
            if (charactersData == null)
                return;

            tmpText.ForceMeshUpdate(true);
            for (var i = 0; i < charactersData.Length; i++)
                ModifyCharacter(i, cachedMeshInfo);

            if (updateGeometry)
                for (var i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                    TmpText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                }

            if (updateVertexData)
                TmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        private void ModifyCharacter(int info, TMP_MeshInfo[] meshInfo)
        {
            for (var i = 0; i < vertexModifiers.Length; i++)
                vertexModifiers[i].ModifyCharacter(charactersData[info],
                    TmpText,
                    textInfo,
                    Progress,
                    meshInfo);
        }

        private void CheckProgress()
        {
            if (IsPlaying)
            {
                internalTime += Time.deltaTime;
                if (internalTime < realTotalAnimationTime || playForever)
                    return;

                if (loop)
                {
                    internalTime = 0;
                }
                else
                {
                    internalTime = realTotalAnimationTime;
                    Progress = 1.0f;
                    Stop();
                    OnAnimationCompleted();
                }
            }
        }

        private void OnAnimationCompleted()
        {
        }

        private void UpdateTime()
        {
            if (!IsPlaying || animationControlled)
                internalTime = Progress * realTotalAnimationTime;
            else
                Progress = internalTime / realTotalAnimationTime;

            if (charactersData == null)
                return;

            for (var i = 0; i < charactersData.Length; i++)
                charactersData[i].UpdateTime(internalTime);
        }

        private void UpdateIfDirty()
        {
            if (!isDirty)
                return;

            if (!gameObject.activeInHierarchy || !gameObject.activeSelf)
                return;

            var currentComponents = GetComponents<TextJuicerVertexModifier>();
            if (vertexModifiers == null || vertexModifiers != currentComponents)
            {
                vertexModifiers = currentComponents;

                for (var i = 0; i < vertexModifiers.Length; i++)
                {
                    var vertexModifier = vertexModifiers[i];

                    if (!updateGeometry && vertexModifier.ModifyGeometry)
                        updateGeometry = true;

                    if (!updateVertexData && vertexModifier.ModifyVertex)
                        updateVertexData = true;
                }
            }

            if (string.IsNullOrEmpty(cachedText) || !cachedText.Equals(TmpText.text))
            {
                TmpText.ForceMeshUpdate();
                textInfo = TmpText.textInfo;
                cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

                var newCharacterDataList = new List<CharacterData>();
                var indexCount = 0;
                for (var i = 0; i < textInfo.characterCount; i++)
                {
                    var targetCharacter = textInfo.characterInfo[i].character;
                    if (targetCharacter == ' ')
                        continue;

                    var characterData = new CharacterData(indexCount,
                        delay + indexCount * delay,
                        duration,
                        playForever,
                        textInfo.characterInfo[i]
                            .materialReferenceIndex,
                        textInfo.characterInfo[i].vertexIndex);
                    newCharacterDataList.Add(characterData);
                    indexCount += 1;
                }

                charactersData = newCharacterDataList.ToArray();
                realTotalAnimationTime = duration +
                                         charactersData.Length * delay;

                cachedText = TmpText.text;
            }

            isDirty = false;
        }

        public void SetDirty()
        {
            isDirty = true;
        }

        #endregion
    }
}