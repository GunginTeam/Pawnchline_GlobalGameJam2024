using UnityEngine;

public class MultiplierCard : BonusCard
{
    [SerializeField] private string _textKey;
    [SerializeField] private float _multiplier;

    protected override void OnConsume()
    {
        _scoreService.SetScoreMultiplier(_multiplier);
    }

    protected override string GetTextKey() => _textKey;
}