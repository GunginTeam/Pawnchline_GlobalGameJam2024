using System;
using UnityEngine;

[Serializable]
public class Humor
{
    public HumorType Type;
    [Range(-2,2)]
    public int Value;
}