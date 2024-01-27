using System;
using UnityEngine;
using Zenject;

public class Session : MonoBehaviour
{
    private const int _amountOfRounds = 3;

    private IScoreService _scoreService;

    private int _currentRoundIndex;
    private Round _currentRound;

    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        _currentRound = new Round(_scoreService);
        _currentRound.SetOnCompleteCallback(EndRound);
        _currentRound.StartTurn();
    }

    private void EndRound()
    {
        _currentRoundIndex++;

        if (_currentRoundIndex >= _amountOfRounds)
        {
            //Game Over
            return;
        }

        StartRound();
    }
}

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
        
        _scoreService.ActionCardPlayed += _ => OnActionCardSelected();
    }

    public void SetOnCompleteCallback(Action onComplete)
    {
        _onComplete = onComplete;
    }

    public bool CanUseBonusCard() => !_bonusActionUsed;
    public void OnBonusCardSelected() => _bonusActionUsed = true;

    private void OnActionCardSelected()
    {
        _onComplete?.Invoke();
        _scoreService.SetScoreMultiplier(_scoreMultiplier);
    }
}