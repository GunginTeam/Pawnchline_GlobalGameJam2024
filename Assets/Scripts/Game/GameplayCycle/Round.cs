using System;

public class Round
{
    const float InitialTurnMultiplier = 0.75f;
    const float BodyTurnMultiplier = 1f;
    const float PunchTurnMultiplier = 1.25f;
    const int AmountOfTurns = 3;

    private IScoreService _scoreService;

    public Turn CurrentTurn;

    private Action _onRoundComplete;
    private Action<int> _onTurnComplete;
    private Action _onBonusActionUsed;

    private int _currentTurnIndex;
    
    public Round(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }
    
    public void SetOnCompleteCallback(Action onRoundComplete, Action<int> onTurnComplete, Action onBonusActionUsed)
    {
        _onRoundComplete = onRoundComplete;
        _onTurnComplete = onTurnComplete;
        _onBonusActionUsed = onBonusActionUsed;
    }

    public void StartTurn()
    {
        CurrentTurn = new Turn(GetMultiplierFromTurnIndex(), _scoreService, _onBonusActionUsed);
        CurrentTurn.SetOnCompleteCallback(EndTurn);
    }

    private void EndTurn()
    {
        CurrentTurn.Dispose();
        _onTurnComplete.Invoke(_currentTurnIndex);

        _currentTurnIndex++;
        
        if (_currentTurnIndex >= AmountOfTurns)
        {
            _onRoundComplete.Invoke();
            return;
        }

        StartTurn();
    }

    private float GetMultiplierFromTurnIndex()
    {
        return _currentTurnIndex switch
        {
            1 => BodyTurnMultiplier,
            2 => PunchTurnMultiplier,
            _ => InitialTurnMultiplier
        };
    }
}