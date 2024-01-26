using System.Collections.Generic;
using Services.Runtime.RemoteVariables;
using TMPro;
using UnityEngine;
using Zenject;

public class ActionCard : BaseCard
{
    [SerializeField] private TMP_Text _text;

    private IRemoteVariablesService _remoteVariablesService;
    
    private ActionCardData _actionCardData;

    [Inject]
    public void Construct(IRemoteVariablesService remoteVariablesService)
    {
        _remoteVariablesService = remoteVariablesService;
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
        //Call the Service
    }
}
