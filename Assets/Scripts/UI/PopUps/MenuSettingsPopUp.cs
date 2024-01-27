using UnityEngine;
using UnityEngine.UI;

public sealed class MenuSettingsPopUp : SettingsPopUp
{
    [SerializeField] private Button _exitButton;
    
    private void Awake()
    {
        _exitButton.onClick.AddListener(HandleExit);
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    private void HandleExit()
    {
#if UNITY_EDITOR
        return;
#endif
        Application.Quit();
    }
}