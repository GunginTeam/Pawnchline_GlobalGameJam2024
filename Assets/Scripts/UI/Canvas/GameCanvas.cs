using Services.Runtime.AudioService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Canvas
{
    public class GameCanvas : BaseCanvas
    {
        [SerializeField] private Button _settingsButton;

        [SerializeField] private GameObject _settingsPopUp;
        [SerializeField] private GameObject _gameOverPopUp;
        
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        protected override void Awake()
        {
            _settingsButton.onClick.AddListener(HandleSettings);
            base.Awake();
        }

        protected override void OnDestroy()
        {
            _settingsButton.onClick.RemoveAllListeners();
            base.OnDestroy();
        }

        public void HandleGameOver()
        {
            CreateView<GameOverPopUp>(_gameOverPopUp, CanvasLayer.PopUps)
                .AddOnExitAction(ExitGame);
        }

        private void HandleSettings()
        {
            PlayLongButton();
            CreateView<GameSettingsPopUp>(_settingsPopUp, CanvasLayer.PopUps)
                .AddOnExitAction(ExitGame);
        }

        private void ExitGame()
        {
            _audioService.StopMusic("MusicLoop", 0.5f);
            _audioService.StopMusic("MusicIntro", 0.5f);
            
            NavigateToScene();
        }
        
        private void PlayLongButton() => _audioService.PlaySFX("ButtonLong");
    }
}