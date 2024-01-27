using System;
using System.Collections.Generic;

public class ScoreService : IScoreService
{
    public event Action<JokeData> ActionCardPlayed;
    public void PlayActionCard(List<HumorType> cardHumor)
    {
        ActionCardPlayed?.Invoke(new JokeData(cardHumor, false));
    }

    public void SetScoreMultiplier(float multiplier)
    {
        
    }

    public void SetReactionScore(int score)
    {
        
    }
}
