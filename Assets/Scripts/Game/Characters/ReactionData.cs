using System;
using UnityEngine;

[Serializable]
public class ReactionData
{
    [Range(-2,2)]
    public int Value;
    public Sprite Sprite;
}