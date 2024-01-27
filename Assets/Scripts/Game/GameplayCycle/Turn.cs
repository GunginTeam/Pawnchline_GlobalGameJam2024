using System;
using System.Collections.Generic;

public class Turn
{
    private IScoreService _scoreService;
    
    private float _scoreMultiplier;
    private bool _bonusActionUsed;
    
    private Action _onComplete;
    
    public Turn(float scoreMultiplier, IScoreService scoreService)
    {
        _scoreMultiplier = scoreMultiplier;
        _scoreService = scoreService;
        
        _scoreService.ActionCardPlayed += OnActionCardSelectedWrapper;
    }
    
    public void Dispose()
    {
        _scoreService.ActionCardPlayed -= OnActionCardSelectedWrapper;
    }

    public void SetOnCompleteCallback(Action onComplete)
    {
        _onComplete = onComplete;
    }

    public bool CanUseBonusCard() => !_bonusActionUsed;
    public void OnBonusCardSelected() => _bonusActionUsed = true;

    private void OnActionCardSelectedWrapper(List<HumorType> _) => OnActionCardSelected();

    private void OnActionCardSelected()
    {
        _onComplete?.Invoke();
        _scoreService.SetScoreMultiplier(_scoreMultiplier);
    }
}