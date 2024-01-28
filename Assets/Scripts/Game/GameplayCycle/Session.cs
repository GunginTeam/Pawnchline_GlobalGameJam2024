using System;
using System.Collections;
using System.Collections.Generic;
using Services.Runtime.AudioService;
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
    private IAudioService _audioService;
    
    private Round _currentRound;

    private int _currentRoundIndex;

    [Inject]
    public void Construct(IScoreService scoreService, IAudioService audioService)
    {
        _scoreService = scoreService;
        _audioService = audioService;
    }

    public Turn GetCurrentTurn() => _currentRound.CurrentTurn;

    private void Start()
    {
        StartRound();
    }

    private void OnDestroy()
    {
        GetCurrentTurn().Dispose();
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
        StartCoroutine(DelayResultCalculation());
    }
    
    private void EndRound()
    {
        Invoke(nameof(NewRoundSFX), 4);

        _currentRoundIndex++;
        OnRoundOver?.Invoke(_currentRoundIndex);

        if (_currentRoundIndex >= AmountOfRounds)
        {
            Invoke(nameof(OpenGameOverPopUp), 4);
            return;
        }

        StartRound();
    }

    private void NewRoundSFX()
    {
        _audioService.PlaySFX("NextRound");
    }

    private void OpenGameOverPopUp() => _gameCanvas.HandleGameOver();

    private IEnumerator DelayResultCalculation()
    {
        yield return null;
        _scoreService.SpreadScore();
    }
}