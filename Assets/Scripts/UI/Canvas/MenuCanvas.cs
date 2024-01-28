using System;
using Services.Runtime.AudioService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Canvas
{
    public class MenuCanvas : BaseCanvas
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _tutorialButton;

        [SerializeField] private GameObject _settingsPopUp;
        [SerializeField] private GameObject _creditsPopUp;
        [SerializeField] private GameObject _tutorialPopUp;
        
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        protected override void Awake()
        {
            _playButton.onClick.AddListener(HandlePlay);
            _settingsButton.onClick.AddListener(HandleSettings);
            _creditsButton.onClick.AddListener(HandleCredits);
            _tutorialButton.onClick.AddListener(HandleTutorial);
            base.Awake();
        }

        protected override void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _creditsButton.onClick.RemoveAllListeners();
            _tutorialButton.onClick.RemoveAllListeners();
            
            base.OnDestroy();
        }

        private void Start()
        {
            _audioService.PlayMusic("MenuMusic");
        }

        private void HandlePlay()
        {
            _audioService.StopMusic("MenuMusic", 0.5f);

            NavigateToScene();
        }

        private void HandleSettings()
        {
            CreateView<SettingsPopUp>(_settingsPopUp, CanvasLayer.PopUps);
        }

        private void HandleCredits()
        {
            CreateView<CreditsPopUp>(_creditsPopUp, CanvasLayer.PopUps);
        }

        private void HandleTutorial()
        {
            CreateView<TutorialPopup>(_tutorialPopUp, CanvasLayer.PopUps);
        }
    }
}