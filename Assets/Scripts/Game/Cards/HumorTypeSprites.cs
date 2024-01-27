using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/HumorTypeSprites", fileName = "HumorTypeSprites")]
public class HumorTypeSprites : ScriptableObject
{
    public List<HumorTypeIcon> HumorSpritesByType = new();
}