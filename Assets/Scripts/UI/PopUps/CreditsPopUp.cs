using Services.Runtime.RemoteVariables;
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
    //
    // protected override void PreOpen()
    // {
    //     base.PreOpen();
    //
    //     _creditsText.text = _remoteVariablesService.GetString("CREDITS_TEXT_0");
    // }
}