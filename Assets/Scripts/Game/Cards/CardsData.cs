using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/CardsData", fileName = "CardsData")]
public class CardsData : ScriptableObject
{
    public List<ActionCardData> Cards = new();
    public List<BonusCard> BonusCards = new();
}