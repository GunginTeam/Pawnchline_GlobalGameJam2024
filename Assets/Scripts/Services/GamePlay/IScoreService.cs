using System;
using System.Collections.Generic;

public interface IScoreService
{
    event Action<JokeData> ActionCardPlayed;

    void PlayActionCard(List<HumorType> cardHumor);
    void SetScoreMultiplier(float multiplier);
    void SetReactionScore(int score);
    void SetIrony();
}