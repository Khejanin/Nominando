using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    public Inventory(List<Item> items)
    {
        this.items = items;
    }
}