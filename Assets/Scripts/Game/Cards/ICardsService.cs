using UnityEngine;

public interface ICardsService
{
    BaseCard GetBonusCardWhapper();
    void SetHolder(Transform holder);
    BaseCard GetCard(bool forceActionCard = false);
}