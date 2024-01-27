using System;
using System.Collections.Generic;

public interface IScoreService
{
    event Action<JokeData> ActionCardPlayed;
    public event Action<float> UpdateUI;
    event Action DiscardDraw;
    void SpreadScore();
    void PlayActionCard(List<HumorType> cardHumor);
    void DiscardDrawPlayed();
    void SetScoreMultiplier(float multiplier);
    void SetReactionScore(float score);
    void SetIrony();
}