using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessItem : Item
{
    public int doorNumber;

    public AccessItem(string name, float weight, int doorNumber) : base(weight, name)
    {
        this.doorNumber = doorNumber;
    }
}
