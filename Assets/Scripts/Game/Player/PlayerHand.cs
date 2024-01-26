using System;
using UnityEngine;
using Zenject;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Transform _cardsHolder;

    private ICardsService _cardsService;

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
            for (int i = 0; i < 5; i++)
            {
                _cardsService.GetCard();
            }
        }
    }

    private void GetInitialTurnHand()
    {
        for (var index = 0; index < _initialCards; index++)
        {
            _cardsService.GetCard(index < _initialForcedBaseCards);
        }
    }
}