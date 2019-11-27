using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public string itemName;

    public int value;

    public Texture2D image;

    public Item(string itemName, int value)
    {
        this.itemName = itemName;
        this.value = value;
    }

    public abstract void use(Entity useOn);

}
