using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : IScoreService
{
    public event Action<JokeData> ActionCardPlayed;
    public event Action DiscardDraw;

    private bool _withIrony;
    private float _roundMultiplier;

    public void PlayActionCard(List<HumorType> cardHumor)
    {
        ActionCardPlayed?.Invoke(new JokeData(cardHumor, _withIrony));
        _withIrony = false;
    }

    public void DiscardDrawPlayed()
    {
        DiscardDraw?.Invoke();
    }

    public void SetScoreMultiplier(float multiplier)
    {
        _roundMultiplier *= multiplier;
    }

    public void SetReactionScore(int score)
    {
        
    }

    public void SetIrony()
    {
        _withIrony = true;
    }
}
