using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public float weight;
    public string itemName;

    public Item(float weight, string itemName)
    {
        this.weight = weight;
        this.itemName = itemName;
    }
}