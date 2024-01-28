using System.Collections;
using Services.Runtime.AudioService;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private IAudioService _audioService;
    private IScoreService _scoreService;
    
    private bool _loweredAudio;

    [Inject]
    public void Construct(IAudioService audioService, IScoreService scoreService)
    {
        _audioService = audioService;
        _scoreService = scoreService;
        _scoreService.PlayScoreSound += OnTurnPassed;
        
        DontDestroyOnLoad(this);
    }

    public void OnDestroy()
    {
        _scoreService.PlayScoreSound -= OnTurnPassed;

        if (_loweredAudio)
        {
            _audioService.AddMusicVolume(5f);
        }
    }

    private void OnTurnPassed(float obj)
    {
        StartCoroutine(LowerMusicVolumeFaded());
        var soundClipString = "Laugh";
        
        if (obj > 0.6)
        {
            soundClipString += "High";
        }
        else if (obj > 0.2 && obj <= 0.6)
        {
            soundClipString += "Good";
        }
        else if (obj > -0.2 && obj <= 0.2)
        {
            soundClipString += "Mid";
        }
        else if (obj > -0.6 && obj <= -0.2)
        {
            soundClipString += "Bad";
        }
        else if (obj <= -0.6)
        {
            soundClipString += "Worse";
        }

        soundClipString += Random.Range(1, 3);
        _audioService.PlaySFX(soundClipString);
    }
    
    IEnumerator LowerMusicVolumeFaded()
    {
        _loweredAudio = true;
        _audioService.AddMusicVolume(-5f);
        yield return new WaitForSeconds(5f);
        _audioService.AddMusicVolume(5f);
        _loweredAudio = false;
    }
}