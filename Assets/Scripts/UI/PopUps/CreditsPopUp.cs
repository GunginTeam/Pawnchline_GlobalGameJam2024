using Services.LocalRemoteVariables;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class CreditsPopUp : BaseView
{
    [SerializeField] private TMP_Text _creditsText;

    private IRemoteVariablesService _remoteVariablesService;
    
    [Inject]
    public void Construct(IRemoteVariablesService remoteVariablesService)
    {
        _remoteVariablesService = remoteVariablesService;
    }
}