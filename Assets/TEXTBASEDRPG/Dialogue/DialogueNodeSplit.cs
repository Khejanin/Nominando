using System.Collections.Generic;
using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueNodeSplit", menuName = "Dialogue/SplitNode")]
public class DialogueNodeSplit : DialogueNode
{
    public List<EventInfo> options = new List<EventInfo>();
}