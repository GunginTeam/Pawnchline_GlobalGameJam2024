using UnityEngine;

public interface ICardsService
{
    BaseCard GetBonusCardThis();
    void SetHolder(Transform holder);
    BaseCard GetCard(bool forceActionCard = false);
}