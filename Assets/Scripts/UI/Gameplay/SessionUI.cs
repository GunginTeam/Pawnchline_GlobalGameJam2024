using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;
using Image = UnityEngine.UI.Image;

public class SessionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _roundText;

    public Image _laughFiller;
    [SerializeField] private Image _bonusActionUse;

    [SerializeField] private Image _openerTurnShownDebug;
    [SerializeField] private Image _bodyTurnShownDebug;
    [SerializeField] private Image _punchTurnShownDebug;

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
        //Commented due to it modifying the scale permanently
        //_laughFiller.transform.parent.DOShakeScale(0.5f, 0.25f);
        _laughFiller.fillAmount += addition;
    }

    private void UpdateRoundText(int roundIndex)
    {
        _currentRoundIndex = roundIndex;

        Invoke(nameof(ApplyRoundTextUpdate), 4);
    }

    private void SetTurnShown(int turnIndex)
    {
        _punchTurnShownDebug.DOFade(turnIndex >= 2 ? 1 : 0, 0.25f);
        _bodyTurnShownDebug.DOFade(turnIndex >= 1 ? 1 : 0, 0.25f);
        _openerTurnShownDebug.DOFade(turnIndex >= 0 ? 1 : 0, 0.25f);

        if (_bonusActionUse.color.a == 0)
        {
            Invoke(nameof(ActivateBonusActionView), 4);
        }
    }

    private void UpdateBonusActionUseDisplay()
    {
        _bonusActionUse.DOFade(0, 0.25f);
    }

    private void ActivateBonusActionView()
    {
        _bonusActionUse.DOFade(1, 0.25f);
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
            _roundText.text = $"ROUND {_currentRoundIndex + 1} / 3";
            _roundText.transform.parent.DOShakeScale(0.5f, 0.5f);

            SetTurnShown(-1);
        }
    }
}