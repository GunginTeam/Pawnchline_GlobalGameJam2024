using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SessionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _roundText;

    [SerializeField] private Image _laughFiller;
    [SerializeField] private Transform _bonusActionUse;

    [SerializeField] private GameObject _openerTurnShownDebug;
    [SerializeField] private GameObject _bodyTurnShownDebug;
    [SerializeField] private GameObject _punchTurnShownDebug;

    private Session _session;
    private IScoreService _scoreService;

    private int _currentRoundIndex;

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
        _session.OnBonusActionUsedEvent += UpdateBonusActionUseDisplay;
        _scoreService.UpdateUI += UpdateLaughFiller;
    }

    private void OnDestroy()
    {
        _session.OnRoundOver -= UpdateRoundText;
        _session.OnTurnOver -= SetTurnShown;
        _session.OnBonusActionUsedEvent -= UpdateBonusActionUseDisplay;
        _scoreService.UpdateUI -= UpdateLaughFiller;
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
        
        if(_bonusActionUse.localScale == Vector3.zero)
        {
            Invoke(nameof(ActivateBonusActionView), 4);
        }
    }

    private void UpdateBonusActionUseDisplay()
    {
        _bonusActionUse.DOScale(0, 0.25f);
    }

    private void ActivateBonusActionView()
    {
        _bonusActionUse.DOScale(1, 0.25f);
    }
    
    private void ResetUI()
    {
        ApplyRoundTextUpdate();
        SetTurnShown(-1);
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