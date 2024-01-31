using UnityEngine;
using UnityEngine.UI;

public sealed class MenuSettingsPopUp : SettingsPopUp
{
    [SerializeField] private Button _exitButton;
    
    private void Awake()
    {
        _exitButton.onClick.AddListener(HandleExit);
    }

    protected override void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
        
        base.OnDestroy();
    }

    private void HandleExit()
    {
#if UNITY_EDITOR
        return;
#endif
        Application.Quit();
    }
}