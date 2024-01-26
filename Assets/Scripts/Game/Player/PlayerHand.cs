using System;
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
    const int _initialForcedBaseCards = 3;

    [Inject]
    public void Construct(ICardsService cardsService)
    {
        _cardsService = cardsService;

        _cardsService.SetHolder(_cardsHolder);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetInitialTurnHand();
        }
    }

    private void GetInitialTurnHand()
    {
        for (var index = 0; index < _initialCards; index++)
        {
            _cardsService.GetCard(index < _initialForcedBaseCards)
                .SetOnSelectCard(SetCurrentCard, CheckUsePreviousCard);
        }
    }

    private void SetCurrentCard(BaseCard card)
    {
        _currentCard = card;
    }

    private void CheckUsePreviousCard()
    {
        var distance = Vector2.Distance(Input.mousePosition, _cardsUsePosition.position);
        
        if (distance <= _cardUseRange)
        {
            _currentCard.Consume();
        }

        _currentCard = null;
    }
}