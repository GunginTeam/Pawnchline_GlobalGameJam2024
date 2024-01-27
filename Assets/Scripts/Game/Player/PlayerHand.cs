using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Transform _cardsHolder;
    [SerializeField] private Transform _cardsUsePosition;

    private List<BaseCard> _handCards = new();

    private Session _currentSession;
    private BaseCard _currentCard;

    private ICardsService _cardsService;
    private IScoreService _scoreService;

    const int _cardUseRange = 50;

    const int _initialCards = 5;
    const int _initialForcedActionCards = 3;

    private int _currentActionCards;

    [Inject]
    public void Construct(ICardsService cardsService, IScoreService scoreService)
    {
        _cardsService = cardsService;
        _scoreService = scoreService;
        _cardsService.SetHolder(_cardsHolder);
        _scoreService.DiscardDraw += DiscardHand;
    }

    private void Awake()
    {
        _currentSession = FindObjectOfType<Session>();
        _currentSession.OnRoundOver += DiscardHand;
    }

    private void OnDestroy()
    {
        _currentSession.OnRoundOver -= DiscardHand;
        _scoreService.DiscardDraw -= DiscardHand;
    }

    private void Start()
    {
        GetInitialTurnHand();
    }

    private void GetInitialTurnHand()
    {
        for (var index = 0; index < _initialCards; index++)
        {
            var forceActionCard = index < _initialForcedActionCards;

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

        if (distance <= _cardUseRange)
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
                    HideHand(false);
                    return;
                }
            }

            _handCards.Remove(_currentCard);
            _currentCard.Consume(isActionCard =>
            {
                if (isActionCard)
                {
                    Invoke(nameof(DelayedShowHand), 4);
                }
                else
                {
                    HideHand(false);
                }
            });

            if (_handCards.Count < _initialCards)
            {
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
        transform.DOLocalMoveY(hide ? -100 : 0, 0.25f);
    }

    private void DiscardHand()
    {
        foreach (var card in _handCards)
        {
            Destroy(card.gameObject);
        }

        _handCards.Clear();

        GetInitialTurnHand();
    }
}