using Services.Runtime.RemoteVariables;
using TMPro;
using UnityEngine;
using Zenject;

public class Card : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private IRemoteVariablesService _remoteVariablesService;
    
    private CardData _data;

    [Inject]
    public void Construct(IRemoteVariablesService remoteVariablesService)
    {
        _remoteVariablesService = remoteVariablesService;
    }
    
    public void Initialize(CardData data)
    {
        _data = data;

        _text.text = _remoteVariablesService.GetString(_data.TextKey);
    }
}
