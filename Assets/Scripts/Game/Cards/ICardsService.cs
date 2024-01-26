using UnityEngine;

public interface ICardsService
{
    void SetHolder(Transform holder);
    BaseCard GetCard(bool actionCard = false);
}