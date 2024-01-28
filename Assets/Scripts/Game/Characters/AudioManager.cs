using System.Collections;
using Services.Runtime.AudioService;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private IAudioService _audioService;
    private IScoreService _scoreService;

    [Inject]
    public void Construct(IAudioService audioService, IScoreService scoreService)
    {
        _audioService = audioService;
        _scoreService = scoreService;
        _scoreService.PlayScoreSound += OnTurnPassed;
    }

    public void OnDestroy()
    {
        _scoreService.PlayScoreSound -= OnTurnPassed;
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
        else if(obj >-0.6&&obj<=-0.2)
        {
            soundClipString += "Bad";
        }
        else if (obj <= -0.6)
        {
            soundClipString += "Worse";
        }
        soundClipString += Random.Range(1, 3);
        Debug.Log("Playing: "+soundClipString);
        _audioService.PlaySFX(soundClipString);
    }

    private void Start()
    {
        PlayGameTheme();
    }

    private void OnDisable()
    {
        _audioService.StopMusic("MusicIntro", 0.5f);
    }

    void PlayGameTheme()
    {
        StartCoroutine(PlayMusicDelayed());
    }

    IEnumerator PlayMusicDelayed()
    {
        yield return new WaitForSeconds(2.25f);

        _audioService.PlayMusic("MusicIntro");
        yield return new WaitForSeconds(81.25f);
        
        _audioService.StopMusic("MusicIntro");
        _audioService.PlayMusic("MusicLoop");
    }

    IEnumerator LowerMusicVolumeFaded()
    {
        _audioService.AddMusicVolume(-5f);
        yield return new WaitForSeconds(5f);
        _audioService.AddMusicVolume(5f);
    }
}