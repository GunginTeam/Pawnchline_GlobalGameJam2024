using System.Collections.Generic;
using Services.Runtime.RemoteVariables;
using TMPro;
using UnityEngine;
using Zenject;

public class ActionCard : BaseCard
{
    [SerializeField] private TMP_Text _text;

    private IRemoteVariablesService _remoteVariablesService;
    private IScoreService _scoreService;
    
    private ActionCardData _actionCardData;

    [Inject]
    public void Construct(IRemoteVariablesService remoteVariablesService, IScoreService scoreService)
    {
        _remoteVariablesService = remoteVariablesService;
        _scoreService = scoreService;
    }
    
    public ActionCard Initialize(ActionCardData data)
    {
        IsAction = true;
        
        _actionCardData = data;

        gameObject.name = _actionCardData.TextKey;
        _text.text = _remoteVariablesService.GetString(_actionCardData.TextKey);

        return this;
    }

    public List<HumorType> GetCardHumor()
    {
        return _actionCardData.HummorTypes;
    }

    protected override void OnConsume()
    {
        _scoreService.PlayActionCard(_actionCardData.HummorTypes);
    }
}
