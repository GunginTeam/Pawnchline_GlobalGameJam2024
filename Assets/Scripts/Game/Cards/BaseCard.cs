using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool IsAction { get; protected set; }
    
    [SerializeField] private Transform _container;

    private Action<BaseCard> _onSelected;
    private Action _onDisSelected;
    
    private bool _isHighlighted;
    private bool _isDragging;
    
    private bool _isConsumed;

    protected abstract void OnConsume();
    
    public void Consume(Action<bool> onComplete = null)
    {
        OnConsume();
        _isConsumed = true;
        
        _container.DOScale(0, 0.25f);
        _container.DORotate(Vector3.forward * 500, 0.5f).SetRelative().OnComplete(() =>
        {
            Destroy(gameObject);
            onComplete?.Invoke(IsAction);
        });
    }
    
    public void SetOnSelectCard(Action<BaseCard> onSelected, Action onDisSelected)
    {
        _onSelected = onSelected;
        _onDisSelected = onDisSelected;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isHighlighted || _isConsumed)
        {
            return;
        }

        HighLight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isHighlighted || _isDragging || _isConsumed)
        {
            return;
        }

        HighLight(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isDragging || _isConsumed)
        {
            return;
        }

        _isDragging = true;
        
        _onSelected.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isDragging || _isConsumed)
        {
            return;
        }

        _isDragging = false;
        
        _onDisSelected.Invoke();
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

        _container.DOLocalMoveY(_isHighlighted ? 50 : 0, 0.25f);
        _container.DOScale(_isHighlighted ? 1.15f : 1, 0.25f);
        _container.DORotate(Vector3.forward * (_isHighlighted ? 90 : 0), 0.25f);
    }

    private void ResetPosition()
    {
        if (_isConsumed)
        {
            return;
        }
        
        _container.DORotate(Vector3.forward * 0, 0.1f).OnComplete(() =>
        {
            _container.DOScale(1, 0.25f);
            _container.DOLocalMove(Vector3.zero, 0.25f);
        });
    }
}