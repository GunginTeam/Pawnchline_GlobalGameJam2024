using System;
using System.Collections.Generic;

public interface IScoreService
{
    event Action<JokeData> ActionCardPlayed;
    event Action DiscardDraw;

    void PlayActionCard(List<HumorType> cardHumor);
    void DiscardDrawPlayed();
    void SetScoreMultiplier(float multiplier);
    void SetReactionScore(int score);
    void SetIrony();
}