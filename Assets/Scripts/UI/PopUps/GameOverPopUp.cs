using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class GameOverPopUp : BaseView
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private List<Transform> _reactions = new();

    [SerializeField]
    private Image _laughFiller;
    
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

    protected override void PreOpen()
    {
        base.PreOpen();
        
        foreach (var reaction in _reactions)
        {
            reaction.DOScale(Vector3.zero, 0f);
        }
    }

    protected override void PostOpen()
    {
        base.PostOpen();

        StartCoroutine(ShowReactions());
    }

    IEnumerator ShowReactions()
    {
        foreach (var reaction in _reactions)
        {
            reaction.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Awake()
    {
        _exitButton.onClick.AddListener(HandleExit);
        PresentPopup();
    }

    private void PresentPopup()
    {
        StartCoroutine(WaitOneFrameAndFill());
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    private void HandleExit()
    {
        Close(()=> _onExit.Invoke());
    }

    IEnumerator WaitOneFrameAndFill()
    {
        yield return null;
        _laughFiller.transform.parent.DOShakeScale(0.5f, 0.25f);
        _laughFiller.fillAmount += _scoreService.GetTotalScore();
    }
}