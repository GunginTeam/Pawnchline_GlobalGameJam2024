using System.Collections.Generic;
using System.Linq;
using Services.LocalRemoteVariables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActionCard : BaseCard
{
    [SerializeField] private GameObject _humorIconPrefab;
    [SerializeField] private Transform _humorIconHolder;
    
    [SerializeField] private TMP_Text _text;

    private IRemoteVariablesService _remoteVariablesService;
    private IScoreService _scoreService;
    
    private ActionCardData _actionCardData;
    private HumorTypeSprites _humorTypeSprites;
    [Inject]
    public void Construct(IRemoteVariablesService remoteVariablesService, IScoreService scoreService, HumorTypeSprites humorTypeSprites)
    {
        _remoteVariablesService = remoteVariablesService;
        _scoreService = scoreService;
        _humorTypeSprites = humorTypeSprites;
    }
    
    public ActionCard Initialize(ActionCardData data)
    {
        IsAction = true;
        
        _actionCardData = data;

        gameObject.name = _actionCardData.TextKey;
        _text.text = _remoteVariablesService.GetString(_actionCardData.TextKey);

        foreach (var humor in GetCardHumor())
        {
            var firstOrDefault = _humorTypeSprites.HumorSpritesByType.FirstOrDefault(x => x.Type == humor).Sprite;
            Instantiate(_humorIconPrefab, _humorIconHolder).GetComponent<Image>().sprite = firstOrDefault;
        }
        
        return this;
    }

    private List<HumorType> GetCardHumor() => _actionCardData.HummorTypes;

    protected override void OnConsume()
    {
        _scoreService.PlayActionCard(_actionCardData.HummorTypes);
    }
}
