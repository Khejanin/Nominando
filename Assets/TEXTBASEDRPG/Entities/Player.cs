using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public Player(string name, Stats stats) : base(name, stats)
    {
    }

    public void talk(NPC npc)
    {
        npc.talkTo();
    }
}
