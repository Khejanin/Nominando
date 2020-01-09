using SwipeMenu;
using UnityEditor;

[CustomEditor(typeof(SwipeHandler))]
[CanEditMultipleObjects]
public class SwipeHandlerEditor : Editor
{
    private SerializedProperty _flickType;
    private SerializedProperty _handleFlicks;
    private SerializedProperty _handleSwipes;
    private SerializedProperty _lockToClosest;
    private SerializedProperty _maxForce;
    private SerializedProperty _requiredForceToFlick;

    // Use this for initialization
    private void OnEnable()
    {
        _handleFlicks = serializedObject.FindProperty("handleFlicks");
        _handleSwipes = serializedObject.FindProperty("handleSwipes");
        _requiredForceToFlick = serializedObject.FindProperty("requiredForceForFlick");
        _flickType = serializedObject.FindProperty("flickType");
        _lockToClosest = serializedObject.FindProperty("lockToClosest");
        _maxForce = serializedObject.FindProperty("maxForce");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_lockToClosest);

        EditorGUILayout.PropertyField(_handleSwipes);

        EditorGUILayout.PropertyField(_handleFlicks);

        if (_handleFlicks.boolValue)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_flickType);
            EditorGUILayout.PropertyField(_requiredForceToFlick);

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(_maxForce);

        serializedObject.ApplyModifiedProperties();
    }
}