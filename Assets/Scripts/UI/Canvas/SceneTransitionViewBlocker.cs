using System;
using DG.Tweening;
using Services.Runtime.AudioService;
using UnityEngine;
using Zenject;

namespace UI.Canvas
{
    public class SceneTransitionViewBlocker : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _interactionBlocker;
        [SerializeField] private Transform _viewBlocker;

        private IAudioService _audioService;
        
        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        private void Start()
        {
            TransitionOff();
        }

        private void TransitionOff(Action onComplete = null)
        {
            _audioService.PlaySFX("CurtainUp");

            _interactionBlocker.blocksRaycasts = true;
            
            _viewBlocker.DOMoveY(Screen.height, 1).SetEase(Ease.InQuart)
                .OnComplete(() =>
                {
                    _interactionBlocker.blocksRaycasts = false;
                    onComplete?.Invoke();
                });
        }

        public void TransitionOn(Action onComplete = null)
        {
            _audioService.PlaySFX("CurtainDown");
            
            _interactionBlocker.blocksRaycasts = true;
            
            _viewBlocker.DOMoveY(0, 1).SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }
    }
}