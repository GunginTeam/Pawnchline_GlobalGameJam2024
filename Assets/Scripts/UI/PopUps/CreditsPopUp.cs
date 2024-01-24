using Services.Runtime.RemoteVariables;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class CreditsPopUp : BaseView
{
    [SerializeField] private TMP_Text _testTest;

    private IRemoteVariablesService _remoteVariablesService;
    
    [Inject]
    public void Construct(IRemoteVariablesService remoteVariablesService)
    {
        _remoteVariablesService = remoteVariablesService;
    }

    protected override void PreOpen()
    {
        base.PreOpen();

        _testTest.text = _remoteVariablesService.GetString("CARD_TEXT_0");
    }
}