using UnityEngine;

public interface ICardsService
{
    void SetHolder(Transform holder);
    BaseCard GetCard(bool forceActionCard = false);
}