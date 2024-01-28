using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Services.Runtime.AudioService;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerHand : MonoBehaviour
{
    const int CardUseRange = 100;
    const int InitialCards = 5;
    const int InitialForcedActionCards = 3;

    [SerializeField] private Transform _cardsHolder;
    [SerializeField] private Transform _cardsUsePosition;

    private List<BaseCard> _handCards = new();

    private Session _currentSession;
    private BaseCard _currentCard;

    private ICardsService _cardsService;
    private IScoreService _scoreService;
    private IAudioService _audioService;

    private int _currentTurn;

    private int _currentActionCards;

    [Inject]
    public void Construct(ICardsService cardsService, IScoreService scoreService, IAudioService audioService)
    {
        _cardsService = cardsService;
        _scoreService = scoreService;
        _audioService = audioService;
        
        _cardsService.SetHolder(_cardsHolder);
        _scoreService.DiscardDraw += DiscardHandWrapper;
    }

    private void Awake()
    {
        _currentSession = FindObjectOfType<Session>();
        _currentSession.OnRoundOver += DiscardHand;
    }

    private void OnDestroy()
    {
        _currentSession.OnRoundOver -= DiscardHand;
        _scoreService.DiscardDraw -= DiscardHandWrapper;
    }

    private void DiscardHandWrapper() => DiscardHand(-1);

    private void Start()
    { 
        transform.DOLocalMoveY(-100, 0f);

        Invoke(nameof(HandleShuffleSFX), 0.75f);
        Invoke(nameof(DelayedShowHand), 1.75f);
        
        GetInitialTurnHand();
        PlayInGameTheme();
    }

    private void OnDisable()
    {
        _cardsService.Dispose();   
    }

    void PlayInGameTheme()
    {
        StartCoroutine(PlayDelayedInGameMusic());
    }

    IEnumerator PlayDelayedInGameMusic()
    {
        yield return new WaitForSeconds(2.25f);

        _audioService.PlayMusic("MusicIntro");
        yield return new WaitForSeconds(81.25f);

        _audioService.StopMusic("MusicIntro");
        _audioService.PlayMusic("MusicLoop");
    }

    private void HandleShuffleSFX() => _audioService.PlaySFX("Shuffle");

    private void GetInitialTurnHand()
    {
        var handSFX = Random.Range(0, 2) == 0;
        
        _audioService.PlaySFX(handSFX ? "DealCardsFast" : "DealCardsSlow");
        
        for (var index = 0; index < InitialCards; index++)
        {
            var forceActionCard = index < InitialForcedActionCards;

            if (FetchCard(forceActionCard).IsAction)
            {
                _currentActionCards++;
            }
        }
    }

    private void SetCurrentCard(BaseCard card)
    {
        _currentCard = card;
        HideHand(true);
    }

    private void CheckUsePreviousCard()
    {
        var distance = Vector2.Distance(Input.mousePosition, _cardsUsePosition.position);

        if (distance <= CardUseRange)
        {
            if (_currentCard.IsAction)
            {
                _currentActionCards--;
            }
            else
            {
                var currentTurn = _currentSession.GetCurrentTurn();
                if (currentTurn.CanUseBonusCard())
                {
                    currentTurn.OnBonusCardSelected();
                }
                else
                {
                    _audioService.PlaySFX("WrongCard");
                    HideHand(false);
                    return;
                }
            }

            _cardsUsePosition.transform.parent.DOShakeScale(0.5f, 0.25f);
            _handCards.Remove(_currentCard);
            _currentCard.Consume(isActionCard =>
            {
                if (_currentTurn < 3)
                {
                    if (isActionCard)
                    {
                        Invoke(nameof(DelayedShowHand), 4);
                    }
                    else
                    {
                        HideHand(false);
                    }
                }
            });

            if (_handCards.Count < InitialCards)
            {
                _audioService.PlaySFX("DrawCard");
                FetchCard(_currentActionCards == 0);
            }
        }
        else
        {
            HideHand(false);
        }

        _currentCard = null;
    }

    private void DelayedShowHand() => HideHand(false);

    private BaseCard FetchCard(bool forceActionCard)
    {
        var card = _cardsService.GetCard(forceActionCard);
        card.SetOnSelectCard(SetCurrentCard, CheckUsePreviousCard);

        _handCards.Add(card);
        return card;
    }

    private void HideHand(bool hide)
    {
        if (!hide)
        {
            _audioService.PlaySFX("ShowHand");
        }
        else
        {
            _audioService.PlaySFX("PickCard" +  Random.Range(0, 4));
        }
        
        transform.DOLocalMoveY(hide ? -100 : 0, 0.25f);
    }

    private void DiscardHand(int currentTurn)
    {
        if (currentTurn >= 0)
        {
            _currentTurn = currentTurn;
        }

        foreach (var card in _handCards)
        {
            Destroy(card.gameObject);
        }

        _handCards.Clear();

        GetInitialTurnHand();
    }
}