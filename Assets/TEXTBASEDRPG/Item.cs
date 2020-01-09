using Namable;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Texture2D image;

    public string itemName;

    public int value;

    public Item(string itemName, int value)
    {
        this.itemName = itemName;
        this.value = value;
    }

    public abstract void use(Entity useOn);
}