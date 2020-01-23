using System.Collections.Generic;
using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueNodeSplit", menuName = "Dialogue/SplitNode")]
public class DialogueNodeSplit : DialogueNode
{
    public List<EventInfo> options = new List<EventInfo>();

    public List<EventInfo> getOptions()
    {
        List<EventInfo> list = new List<EventInfo>();
        for (int i = 0; i < options.Count-hideOptions; i++)
        {
            list.Add(options[i]); 
        }

        return list;
    }
    
    public int hideOptions = 0;
}