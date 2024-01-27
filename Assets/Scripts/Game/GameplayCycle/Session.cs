using System;
using UnityEngine;
using Zenject;

public class Session : MonoBehaviour
{
    const int AmountOfRounds = 3;

    public Action OnRoundOver;

    private IScoreService _scoreService;
    
    private Round _currentRound;

    private int _currentRoundIndex;

    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public Turn GetCurrentTurn() => _currentRound.CurrentTurn;

    private void Start()
    {
        Debug.Log($"Session STARTED");

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
        OnRoundOver.Invoke();

        _currentRoundIndex++;

        if (_currentRoundIndex >= AmountOfRounds)
        {
            Debug.Log("SESSION OVER");
            return;
        }

        StartRound();
    }
}