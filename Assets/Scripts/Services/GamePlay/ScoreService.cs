using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreService : IScoreService
{
    public event Action<JokeData> ActionCardPlayed;
    public event Action DiscardDraw;
    public event Action<float> UpdateUI;

    private bool _withIrony;
    
    private float _roundMultiplier=1;
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
        var currentTurnScore = (_reactionScore * _roundMultiplier)/5;
        Debug.Log("Increasing meter by: "+currentTurnScore);
        Debug.Log("The reaction score was: "+_reactionScore);
        Debug.Log("The multiplier was: "+_roundMultiplier);
        UpdateUI.Invoke(currentTurnScore);
        
        _roundMultiplier = 1;
    }

    public void SetScoreMultiplier(float multiplier)
    {
        _roundMultiplier *= multiplier;
        Debug.Log("Set the multiplier to "+_roundMultiplier);
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
