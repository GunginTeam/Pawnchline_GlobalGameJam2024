using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform _container;

    private bool _isHighlighted;
    private bool _isDragging;

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
        if (!_isHighlighted || _isDragging)
        {
            return;
        }

        HighLight(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isDragging)
        {
            return;
        }

        _isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isDragging)
        {
            return;
        }

        _isDragging = false;

        ResetPosition();
    }

    private void Update()
    {
        if (_isDragging)
        {
            _container.DOMove(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), 0.25f);
        }
    }

    private void HighLight(bool highlight)
    {
        _isHighlighted = highlight;

        _container.DOMoveY(_isHighlighted ? 50 : 0, 0.25f);
        _container.DOScale(_isHighlighted ? 1.15f : 1, 0.25f);
        _container.DORotate(Vector3.forward * (_isHighlighted ? 90 : 0), 0.25f);
    }

    private void ResetPosition()
    {
        _container.DORotate(Vector3.forward * 0, 0.1f).OnComplete(() =>
        {
            _container.DOScale(1, 0.25f);
            _container.DOLocalMove(Vector3.zero, 0.25f);
        });
    }
}