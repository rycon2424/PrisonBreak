using System;
using UnityEngine;

[Serializable]
public class HeightPass
{
    public enum PassType {PerlinBased,RandomBased,ImageBased};

    public string name;
    public float height;
    public float detail;
    public Sprite spritePass;

    public PassType passType;
}