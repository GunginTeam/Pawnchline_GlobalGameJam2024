using UnityEngine;
using Zenject;

public class Session : MonoBehaviour
{
    private const int _amountOfRounds = 3;

    private IScoreService _scoreService;

    private int _currentRoundIndex;
    private Round _currentRound;

    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    private void Start()
    {
        Debug.Log($"Session STARTED");

        StartRound();
    }

    private void StartRound()
    {
        _currentRound = new Round(_scoreService);
        _currentRound.SetOnCompleteCallback(EndRound);
        _currentRound.StartTurn();
    }

    private void EndRound()
    {
        _currentRoundIndex++;

        if (_currentRoundIndex >= _amountOfRounds)
        {
            Debug.Log("SESSION OVER");
            return;
        }

        StartRound();
    }
}