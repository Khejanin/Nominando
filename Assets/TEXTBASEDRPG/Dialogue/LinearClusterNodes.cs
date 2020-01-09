using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New LinearCluster", menuName = "Dialogue/LinearCluster")]
public class LinearClusterNodes : ScriptableObject
{
    public List<DialogueNode> nodes;
}