using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : IScoreService
{
    public event Action<JokeData> ActionCardPlayed;
    public event Action DiscardDraw;
    public event Action<float> UpdateUI;

    private bool _withIrony;
    
    private float _roundMultiplier;
    private float _reactionScore;

    public void PlayActionCard(List<HumorType> cardHumor)
    {
        ActionCardPlayed?.Invoke(new JokeData(cardHumor, _withIrony));
        _withIrony = false;
    }

    public void DiscardDrawPlayed()
    {
        DiscardDraw?.Invoke();
    }

    public void SpreadScore()
    {
        var currentTurnScore = (_reactionScore * _roundMultiplier)/3;
        Debug.Log("Increasing meter by: ");
        UpdateUI.Invoke(currentTurnScore);
        
        _roundMultiplier = 1;
    }

    public void SetScoreMultiplier(float multiplier)
    {
        _roundMultiplier *= multiplier;
    }

    public void SetReactionScore(float score)
    {
        _reactionScore = score;
    }

    public void SetIrony()
    {
        _withIrony = true;
    }
}
