using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/CardsData", fileName = "CardsData")]
public class CardsData : ScriptableObject
{
    public ActionCard CardPrefab;
    public List<ActionCardData> Cards = new();
    public List<BonusCard> BonusCards = new();
}