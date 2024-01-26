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
        _currentRound.StartTurn();
        _currentRound.SetOnCompleteCallback(EndRound);
    }

    private void EndRound()
    {
        _currentRoundIndex++;

        if (_currentRoundIndex >= _amountOfRounds)
        {
            //GameOver
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
    
    private IScoreService _scoreService;

    private Action _onComplete;

    private Turn CurrentTurn;

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
        CurrentTurn = new Turn(GetMultiplierFromTurnIndex());
        
        _currentTurnIndex++;
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
    private float _scoreMultiplier;
    
    public Turn(float scoreMultiplier)
    {
        _scoreMultiplier = scoreMultiplier;
    }
}