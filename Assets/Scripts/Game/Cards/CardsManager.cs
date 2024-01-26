using System.Linq;
using UnityEngine;
using Zenject;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [Inject]
    public void Contruct(CardsData cardsData)
    {
        Instantiate(_cardPrefab).Initialize(cardsData.Cards.First());
    }
}
