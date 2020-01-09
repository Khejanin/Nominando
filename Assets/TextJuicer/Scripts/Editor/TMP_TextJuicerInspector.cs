using UnityEditor;
using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TMP_TextJuicer), true)]
    public sealed class TMP_TextJuicerInspector : Editor
    {
        private SerializedProperty animationControlledSerializedProperty;
        private SerializedProperty delaySerializedProperty;
        private SerializedProperty durationSerializedProperty;
        private SerializedProperty loopSerializedProperty;
        private SerializedProperty playForeverSerializedProperty;
        private SerializedProperty playWhenReadySerializedProperty;
        private SerializedProperty progressSerializedProperty;
        private SerializedProperty textComponentSerializedProperty;
        private TMP_TextJuicer textJuicer;

        private void OnEnable()
        {
            textJuicer = (TMP_TextJuicer) target;

            textComponentSerializedProperty = serializedObject.FindProperty("tmpText");
            durationSerializedProperty = serializedObject.FindProperty("duration");
            delaySerializedProperty = serializedObject.FindProperty("delay");
            progressSerializedProperty = serializedObject.FindProperty("progress");
            playWhenReadySerializedProperty = serializedObject.FindProperty("playWhenReady");
            loopSerializedProperty = serializedObject.FindProperty("loop");
            playForeverSerializedProperty = serializedObject.FindProperty("playForever");
            animationControlledSerializedProperty = serializedObject.FindProperty("animationControlled");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(textComponentSerializedProperty);
            EditorGUILayout.PropertyField(durationSerializedProperty);
            EditorGUILayout.PropertyField(delaySerializedProperty);
            EditorGUILayout.PropertyField(animationControlledSerializedProperty);
            if (animationControlledSerializedProperty.boolValue)
            {
                EditorGUILayout.PropertyField(progressSerializedProperty);
            }
            else
            {
                EditorGUILayout.PropertyField(playWhenReadySerializedProperty);
                EditorGUILayout.PropertyField(loopSerializedProperty);
                EditorGUILayout.PropertyField(playForeverSerializedProperty);
            }

            serializedObject.ApplyModifiedProperties();

            if (animationControlledSerializedProperty.boolValue)
                return;

            if (!Application.isPlaying)
                return;

            if (!textJuicer.IsPlaying)
            {
                if (GUILayout.Button("Play")) textJuicer.Play();
            }
            else
            {
                if (GUILayout.Button("Stop")) textJuicer.Stop();
            }
        }
    }
#endif
}