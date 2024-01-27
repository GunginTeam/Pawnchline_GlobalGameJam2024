using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CardsService : ICardsService
{
    const int _chanceToGetActionCard = 10;
    
    private Dictionary<ActionCardData, bool> _actionCardsDeck = new();
    private Dictionary<BonusCard, bool> _bonusCardsDeck = new();

    private IInstancer _instancer;
    private CardsData _cardsData;
    private Transform _cardsHolder;

    [Inject]
    public void Construct(IInstancer instancer, CardsData cardsData)
    {
        _instancer = instancer;
        _cardsData = cardsData;
        
        foreach (var cardData in _cardsData.Cards)
        {
            _actionCardsDeck.Add(cardData, true);
        }

        foreach (var bonusCardData in _cardsData.BonusCards)
        {
            _bonusCardsDeck.Add(bonusCardData, true);
        }
    }

    public void SetHolder(Transform holder)
    {
        _cardsHolder = holder;
    }
    
    public BaseCard GetCard(bool forceActionCard = false)
    {
        if (forceActionCard)
        {
            return GetActionCard();
        }

        if (Random.Range(0, 10) >= _chanceToGetActionCard)
        {
            return GetBonusCard();
        }

        return GetActionCard();
    }
    
    public BaseCard GetBonusCardThis() => GetBonusCard();

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
            if (iteration > _actionCardsDeck.Count +1)
            {
                Debug.LogError("No available ACTION cards");
                break;
            }
        }

        _actionCardsDeck[selectedCard.Key] = false;

        return _instancer.Create<ActionCard>(_cardsData.CardPrefab.gameObject, _cardsHolder).Initialize(selectedCard.Key);
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

        return _instancer.Create<BonusCard>(selectedCard.Key.gameObject, _cardsHolder).Initialize();
    }
}