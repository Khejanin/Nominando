using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(LinearClusterNodes))]
public class CustomDialogueEditor : Editor
{
    private LinearClusterNodes myNodes;
    private string NodeName = "";
    private string NodePath = "";


    private SerializedObject nodesSO;


    private readonly List<bool> showNode = new List<bool>();
    private Editor testEditor;
    private Editor tmpEditor;

    private void OnEnable()
    {
        nodesSO = new SerializedObject(target);
        myNodes = (LinearClusterNodes) target;
        foreach (var node in myNodes.nodes) showNode.Add(false);
    }

    public override void OnInspectorGUI()
    {
        nodesSO.Update();

        EditorGUILayout.BeginVertical();

        var i = 0;
        if (myNodes.nodes != null)
            foreach (var currentNode in myNodes.nodes)
            {
                Debug.Log(i);
                showNode[i] = EditorGUILayout.Foldout(showNode[i], "Point #" + i);
                if (currentNode && showNode[i])
                {
                    currentNode.dialogueText = EditorGUILayout.TextField("Text:", currentNode.dialogueText);
                    if (i > 0)
                        currentNode.next = (DialogueNode) EditorGUILayout.ObjectField("Next Node : ",
                            myNodes.nodes[i - 1], currentNode.GetType(), true, GUILayout.MaxHeight(25.0f));
                    else
                        currentNode.next = (DialogueNode) EditorGUILayout.ObjectField("Next Node : ", null,
                            currentNode.GetType(), true, GUILayout.MaxHeight(25.0f));

                    Debug.Log(currentNode.GetType());
                    if (currentNode.GetType() == typeof(DialogueNodeSplit))
                    {
                        Debug.Log("I HAVE ENTERED");
                        var currentNodeSplit = currentNode as DialogueNodeSplit;


                        if (currentNodeSplit.options.Count > 0)
                            Debug.Log(currentNodeSplit.options[currentNodeSplit.options.Count - 1]);

                        var j = 0;

                        EditorGUILayout.Space();

                        EditorGUILayout.LabelField("Options : ");

                        EditorGUILayout.Space();


                        foreach (var option in currentNodeSplit.options)
                        {
                            if (option != null)
                            {
                                CreateEditor(option).OnInspectorGUI();
                                EditorGUILayout.Space();
                                EditorGUILayout.Separator();
                                EditorGUILayout.Space();
                            }
                            else
                            {
                                CreateEditor(currentNodeSplit.options[j]).OnInspectorGUI();
                            }

                            j++;
                        }

                        if (currentNodeSplit.options.Count != 4 && GUILayout.Button("Add Option"))
                            /*DialogueOption createdScriptableDialogueOption =
                                (DialogueOption) ScriptableObject.CreateInstance<DialogueOption>();
                            currentNodeSplit.options.Add(createdScriptableDialogueOption);
                            AssetDatabase.CreateAsset(createdScriptableDialogueOption,
                                NodePath + NodeName + ".asset");*/
                            showNode.Add(false);
                        //testEditor = CreateEditor(currentNodeSplit.options.ToArray());
                    }

                    if (GUILayout.Button("Remove Node"))
                    {
                        myNodes.nodes.RemoveAt(i);
                        AssetDatabase.DeleteAsset(NodePath + i + ".asset");
                        i--;
                    }
                }

                EditorGUILayout.Separator();
                i++;
            }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.Separator();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Add Node"))
            if (NodeName != "" && !File.Exists("Assets/MyStuff/" + NodeName + ".asset"))
            {
                var createdScriptableDialogueNode =
                    CreateInstance<DialogueNode>();
                myNodes.nodes.Add(createdScriptableDialogueNode);
                AssetDatabase.CreateAsset(createdScriptableDialogueNode, NodePath + NodeName + ".asset");
                showNode.Add(false);
            }

        if (GUILayout.Button("Add Split Node"))
            if (NodeName != "" && !File.Exists("Assets/MyStuff/" + NodeName + ".asset"))
            {
                var createdScriptableDialogueNode =
                    CreateInstance<DialogueNodeSplit>();
                myNodes.nodes.Add(createdScriptableDialogueNode);
                AssetDatabase.CreateAsset(createdScriptableDialogueNode, NodePath + NodeName + ".asset");
                showNode.Add(false);
            }

        NodeName = EditorGUILayout.TextField("Node Name : ", NodeName);
        NodePath = EditorGUILayout.TextField("Node Path: ", NodePath);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            EditorUtility.SetDirty(myNodes);
        }

        nodesSO.ApplyModifiedProperties();
    }
}
#endif