using TMPro;
using UnityEngine;

public class SessionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _roundText;

    [SerializeField] private GameObject _openerTurnShownDebug;
    [SerializeField] private GameObject _bodyTurnShownDebug;
    [SerializeField] private GameObject _punchTurnShownDebug;

    private Session _session;
    
    private int _currentRoundIndex;
    
    public void UpdateRoundText(int roundIndex)
    {
        _currentRoundIndex = roundIndex;

        Invoke(nameof(ApplyRoundTextUpdate),4);
    }

    public void SetTurnShown(int turnIndex)
    {
        _openerTurnShownDebug.SetActive(turnIndex >= 0);
        _bodyTurnShownDebug.SetActive(turnIndex >= 1);
        _punchTurnShownDebug.SetActive(turnIndex >= 2);
    }

    public void ResetUI()
    {
        ApplyRoundTextUpdate();
        SetTurnShown(-1);
    }

    private void Start()
    {
        ResetUI();

        _session.OnRoundOver += UpdateRoundText;
        _session.OnTurnOver += SetTurnShown;
    }

    private void Awake()
    {
        _session = FindObjectOfType<Session>();
    }

    private void OnDestroy()
    {
        _session.OnRoundOver -= UpdateRoundText;
        _session.OnTurnOver -= SetTurnShown;
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