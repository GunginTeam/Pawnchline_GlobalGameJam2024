using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class GameOverPopUp : BaseView
{
    [SerializeField] private Button _exitButton;
    
    private IScoreService _scoreService;
    private Action _onExit;

    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public void AddOnExitAction(Action onExit)
    {
        _onExit = onExit;
    }

    private void Awake()
    {
        _exitButton.onClick.AddListener(HandleExit);
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    private void HandleExit()
    {
        Close(()=> _onExit.Invoke());
    }
}