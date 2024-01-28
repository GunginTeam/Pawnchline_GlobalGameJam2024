using Services.Runtime.AudioService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsPopUp : BaseView
{
    [SerializeField] private GameObject _musicMutedDisplay;
    [SerializeField] private GameObject _sfxMutedDisplay;
    
    [SerializeField] private Button _musicAudio;
    [SerializeField] private Button _sfxAudio;
    
    private IAudioService _audioService;

    [Inject]
    protected  void Construct(IAudioService audioService)
    {
        _audioService = audioService;

        _musicAudio.onClick.AddListener(HandleMuteMusic);
        _sfxAudio.onClick.AddListener(HandleMuteSFX);
        
        _musicMutedDisplay.SetActive(PlayerPrefs.GetInt("MusicMuted") == 0);
        _sfxMutedDisplay.SetActive(PlayerPrefs.GetInt("SFXMuted") == 0);
        
    }

    protected void OnDestroy()
    {
        _musicAudio.onClick.RemoveListener(HandleMuteMusic);
        _sfxAudio.onClick.RemoveListener(HandleMuteSFX);
    }
    
    private void HandleMuteMusic()
    {
        var muted = _audioService.MuteMusic();
        _audioService.PlaySFX("ButtonShort");
        _musicMutedDisplay.SetActive(muted);
    }

    private void HandleMuteSFX()
    {
        var muted = _audioService.MuteSFX();
        _audioService.PlaySFX("ButtonShort");
        _sfxMutedDisplay.SetActive(muted);
    }
}