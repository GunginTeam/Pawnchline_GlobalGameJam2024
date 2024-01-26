using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _background;

    private bool _isHighlighted;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isHighlighted)
        {
            return;
        }

        HighLight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isHighlighted)
        {
            return;
        }

        HighLight(false);
    }

    private void HighLight(bool highlight)
    {
        _isHighlighted = highlight;
        
        transform.DOMoveY(_isHighlighted ? 50 : 0, 0.25f);
        transform.DORotate(Vector3.forward * (_isHighlighted ? 90 : 0), 0.25f);
    }
}