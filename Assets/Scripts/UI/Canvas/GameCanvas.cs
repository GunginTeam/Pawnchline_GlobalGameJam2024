using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvas
{
    public class GameCanvas : BaseCanvas
    {
        [SerializeField] private Button _settingsButton;
        
        [SerializeField] private GameObject _settingsPopUp;
        [SerializeField] private GameObject _gameOverPopUp;
        
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
                .AddOnExitAction(NavigateToScene);
        }

        private void HandleSettings()
        {
           CreateView<GameSettingsPopUp>(_settingsPopUp, CanvasLayer.PopUps)
               .AddOnExitAction(NavigateToScene);
        }
    }
}