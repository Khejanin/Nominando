using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public Dialogue dialogue;

    public string characterName;

    public int hp;

    public Inventory inventory;

    public Texture2D image;

    [System.Serializable]
    public class Stats
    {
        public int Strength;
        public int Endurance;
        public int Speed;
        public int Intelligence;
        public int Arcana;
    }

    public Stats stats;

    public Entity(string name, Stats stats)
    {
        this.characterName = name;
        this.stats = stats;
    }

}
