using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : IScoreService
{
    public event Action<List<HumorType>> ActionCardPlayed;
    
    public void PlayActionCard(List<HumorType> cardHumor)
    {
        ActionCardPlayed?.Invoke(cardHumor);
    }

    public void SetScoreMultiplier(float multiplier)
    {
        Debug.Log($"Add Multiplier: x{multiplier}");
    }

    public void SetReactionScore(int score)
    {
        
    }
}
