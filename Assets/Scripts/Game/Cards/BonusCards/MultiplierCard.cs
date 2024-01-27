using UnityEngine;

public class MultiplierCard : BonusCard
{
    [SerializeField] private float _multiplier;

    protected override void OnConsume()
    {
        _scoreService.SetScoreMultiplier(_multiplier);
    }
}