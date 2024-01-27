using System;
using UI.Canvas;
using UnityEngine;
using Zenject;

public class Session : MonoBehaviour
{
    const int AmountOfRounds = 3;

    public event Action<int> OnRoundOver;
    public event Action<int> OnTurnOver;
    public event Action OnBonusActionUsedEvent;
    
    [SerializeField] private GameCanvas _gameCanvas;

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
        StartRound();
    }

    private void StartRound()
    {
        _currentRound = new Round(_scoreService);
        _currentRound.SetOnCompleteCallback(EndRound, EndTurn, OnBonusActionUsed);
        _currentRound.StartTurn();
    }

    private void OnBonusActionUsed()
    {
        OnBonusActionUsedEvent?.Invoke();
    }
    
    private void EndTurn(int turnIndex)
    {
        OnTurnOver?.Invoke(turnIndex);
        _scoreService.SpreadScore();
    }
    private void EndRound()
    {
        _currentRoundIndex++;
        OnRoundOver?.Invoke(_currentRoundIndex);

        if (_currentRoundIndex >= AmountOfRounds)
        {
            Invoke(nameof(OpenGameOverPopUp), 4);
            return;
        }

        StartRound();
    }

    private void OpenGameOverPopUp() => _gameCanvas.HandleGameOver();
}