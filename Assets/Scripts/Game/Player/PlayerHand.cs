using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Transform _cardsHolder;
    [SerializeField] private Transform _cardsUsePosition;

    private BaseCard _currentCard;

    private ICardsService _cardsService;

    const int _cardUseRange = 50;

    const int _initialCards = 5;
    const int _initialForcedActionCards = 3;

    private int _currentActionCards;

    [Inject]
    public void Construct(ICardsService cardsService)
    {
        _cardsService = cardsService;

        _cardsService.SetHolder(_cardsHolder);
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

            _currentCard.Consume(()=> HideHand(false));

            FetchCard(_currentActionCards == 0);
        }
        else
        {
            HideHand(false);
        }

        _currentCard = null;
    }

    private BaseCard FetchCard(bool forceActionCard)
    {
        var card = _cardsService.GetCard(forceActionCard);
        card.SetOnSelectCard(SetCurrentCard, CheckUsePreviousCard);

        return card;
    }

    private void HideHand(bool hide)
    {
        transform.DOLocalMoveY(hide ? -100 : 0, 0.25f);
    }
}