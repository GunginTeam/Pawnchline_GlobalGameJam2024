using UnityEngine;

public interface ICardsService
{
    void Dispose();
    void SetHolder(Transform holder);
    BaseCard GetCard(bool forceActionCard = false);
}