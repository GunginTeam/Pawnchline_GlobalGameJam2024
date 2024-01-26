using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CardsManager : MonoBehaviour
{
    [Range(5, 10)]
    [SerializeField] private int _chanceToGetBonusCard = 8;
    
    [SerializeField] private ActionCard _actionCardPrefab;

    private Dictionary<ActionCardData, bool> _actionCardsDeck = new();
    private Dictionary<BonusCard, bool> _bonusCardsDeck = new();

    private IInstancer _instancer;
    private CardsData _cardsData;

    [Inject]
    public void Construct(IInstancer instancer, CardsData cardsData)
    {
        _instancer = instancer;
        _cardsData = cardsData;
    }

    private void Start()
    {
        foreach (var cardData in _cardsData.Cards)
        {
            _actionCardsDeck.Add(cardData, true);
        }

        foreach (var bonusCardData in _cardsData.BonusCards)
        {
            _bonusCardsDeck.Add(bonusCardData, true);
        }
    }

    public BaseCard GetCard(bool actionCard = false)
    {
        if (actionCard)
        {
            return GetActionCard();
        }

        if (Random.Range(0, 10) >= _chanceToGetBonusCard)
        {
            return GetBonusCard();
        }

        return GetActionCard();
    }

    private BaseCard GetActionCard()
    {
        var range = Random.Range(0, _actionCardsDeck.Count);
        var selectedCard = _actionCardsDeck.ElementAt(range);

        var iteration = 0;
        while (!selectedCard.Value)
        {
            range = Random.Range(0, _actionCardsDeck.Count);
            selectedCard = _actionCardsDeck.ElementAt(range);

            iteration++;
            if (iteration > _actionCardsDeck.Count)
            {
                Debug.LogError("No available ACTION cards");
            }
        }

        _actionCardsDeck[selectedCard.Key] = false;

        return _instancer.Create<ActionCard>(_actionCardPrefab.gameObject, transform).Initialize(selectedCard.Key);
    }

    private BaseCard GetBonusCard()
    {
        var range = Random.Range(0, _bonusCardsDeck.Count);
        var selectedCard = _bonusCardsDeck.ElementAt(range);

        var iteration = 0;
        while (!selectedCard.Value)
        {
            range = Random.Range(0, _bonusCardsDeck.Count);
            selectedCard = _bonusCardsDeck.ElementAt(range);

            iteration++;
            if (iteration > _bonusCardsDeck.Count)
            {
                return GetActionCard();
            }
        }

        _bonusCardsDeck[selectedCard.Key] = false;

        return _instancer.Create<BonusCard>(selectedCard.Key.gameObject, transform).Initialize();
    }
}