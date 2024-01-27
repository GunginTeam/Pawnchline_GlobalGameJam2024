using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : IScoreService
{
    public event Action<JokeData> ActionCardPlayed;

    public void PlayActionCard(List<HumorType> cardHumor)
    {
        ActionCardPlayed?.Invoke(new JokeData(cardHumor, false));
    }

    public void SetScoreMultiplier(float multiplier)
    {
        Debug.Log($"Add Multiplier: x{multiplier}");
    }

    public void SetReactionScore(int score)
    {
        
    }
}
