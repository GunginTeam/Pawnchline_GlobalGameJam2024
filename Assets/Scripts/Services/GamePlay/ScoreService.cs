using System;
using System.Collections.Generic;

public class ScoreService : IScoreService
{
    public event Action<List<HumorType>> ActionCardPlayed;
    public void PlayActionCard(List<HumorType> cardHumor)
    {
        ActionCardPlayed?.Invoke(cardHumor);
    }
}
