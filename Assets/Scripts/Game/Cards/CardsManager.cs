using System.Linq;
using UnityEngine;
using Zenject;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;

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
        var firstOrDefault = _cardsData.Cards.FirstOrDefault();
        _instancer.Create<Card>(_cardPrefab.gameObject, transform).Initialize(firstOrDefault);
    }
}