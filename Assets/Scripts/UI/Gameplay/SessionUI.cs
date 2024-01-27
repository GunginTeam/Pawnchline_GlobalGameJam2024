using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SessionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _roundText;

    [SerializeField] private Image _laughFiller;
    [SerializeField] private GameObject _bonusActionUse;

    [SerializeField] private GameObject _openerTurnShownDebug;
    [SerializeField] private GameObject _bodyTurnShownDebug;
    [SerializeField] private GameObject _punchTurnShownDebug;

    private Session _session;
    private IScoreService _scoreService;

    private int _currentRoundIndex;
    private bool _bonusActionAvailable;

    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    private void Awake()
    {
        _session = FindObjectOfType<Session>();
    }

    private void Start()
    {
        ResetUI();

        _session.OnRoundOver += UpdateRoundText;
        _session.OnTurnOver += SetTurnShown;
        _scoreService.UpdateUI += UpdateLaughFiller;
    }

    private void OnDestroy()
    {
        _session.OnRoundOver -= UpdateRoundText;
        _session.OnTurnOver -= SetTurnShown;
        _scoreService.UpdateUI -= UpdateLaughFiller;
    }

    private void Update()
    {
        UpdateBonusActionUseDisplay(_session.GetCurrentTurn().CanUseBonusCard());
    }

    private void UpdateLaughFiller(float addition)
    {
        _laughFiller.fillAmount += addition;
    }

    private void UpdateRoundText(int roundIndex)
    {
        _currentRoundIndex = roundIndex;

        Invoke(nameof(ApplyRoundTextUpdate), 4);
    }

    private void SetTurnShown(int turnIndex)
    {
        _openerTurnShownDebug.SetActive(turnIndex >= 0);
        _bodyTurnShownDebug.SetActive(turnIndex >= 1);
        _punchTurnShownDebug.SetActive(turnIndex >= 2);
    }

    private void UpdateBonusActionUseDisplay(bool isVisible)
    {
        if (!isVisible && !_bonusActionUse.activeSelf || isVisible && _bonusActionUse.activeSelf)
        {
            return;
        }
        
        if (isVisible && _bonusActionAvailable)
        {
            _bonusActionAvailable = false;
            Invoke(nameof(ActivateBonusActionView), 4);
            return;
        }

        _bonusActionAvailable = true;
        _bonusActionUse.SetActive(false);
    }

    private void ActivateBonusActionView()
    {
        _bonusActionUse.SetActive(true);
    }
    
    private void ResetUI()
    {
        ApplyRoundTextUpdate();
        SetTurnShown(-1);
        UpdateBonusActionUseDisplay(true);
    }

    private void ApplyRoundTextUpdate()
    {
        if (_currentRoundIndex + 1 < 4)
        {
            _roundText.text = $"ROUND {_currentRoundIndex + 1}";
            SetTurnShown(-1);
        }
    }
}