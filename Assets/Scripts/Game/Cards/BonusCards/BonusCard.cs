using Services.LocalRemoteVariables;
using TMPro;
using UnityEngine;
using Zenject;

public abstract class BonusCard : BaseCard
{
    [SerializeField] private TMP_Text _cardText;
    
    protected IScoreService _scoreService;
    protected IRemoteVariablesService _remoteVariables;
    
    [Inject]
    public void Construct(IScoreService scoreService, IRemoteVariablesService remoteVariables)
    {
        _scoreService = scoreService;
        _remoteVariables = remoteVariables;
    }
    
    public BonusCard Initialize()
    {
        _cardText.text = _remoteVariables.GetString(GetTextKey());
        
        return this;
    }

    protected override void OnConsume()
    {
        
    }

    protected abstract string GetTextKey();
}