using System;
using System.Collections.Generic;

public interface IScoreService
{
    event Action<List<HumorType>> ActionCardPlayed;

    void PlayActionCard(List<HumorType> cardHumor);
}