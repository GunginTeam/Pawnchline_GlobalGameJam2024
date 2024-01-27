using System;

public class Round
{
    const float _initialTurnMultiplier = 0.75f;
    const float _bodyTurnMultiplier = 1f;
    const float _punchTurnMultiplier = 1.25f;
    const int _amountOfTurns = 3;

    private IScoreService _scoreService;

    public Turn CurrentTurn;

    private Action _onComplete;

    private int _currentTurnIndex;
    
    public Round(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }
    
    public void SetOnCompleteCallback(Action onComplete)
    {
        _onComplete = onComplete;
    }

    public void StartTurn()
    {
        CurrentTurn = new Turn(GetMultiplierFromTurnIndex(), _scoreService);
        CurrentTurn.SetOnCompleteCallback(EndTurn);
    }

    private void EndTurn()
    {
        CurrentTurn.Dispose();
        
        _currentTurnIndex++;

        if (_currentTurnIndex >= _amountOfTurns)
        {
            _onComplete.Invoke();
            return;
        }

        StartTurn();
    }

    private float GetMultiplierFromTurnIndex()
    {
        return _currentTurnIndex switch
        {
            1 => _bodyTurnMultiplier,
            2 => _punchTurnMultiplier,
            _ => _initialTurnMultiplier
        };
    }
}