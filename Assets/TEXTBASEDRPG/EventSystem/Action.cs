using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Entities/Action")]
public class Action : ScriptableObject, IEventHolder
{

    public EventInfo heldEvent;
    
    public EventInfo GetEvent()
    {
        return heldEvent;
    }
    
}
