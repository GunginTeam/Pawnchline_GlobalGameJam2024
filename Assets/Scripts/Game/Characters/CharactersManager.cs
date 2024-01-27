using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] private List<Character> _characters;
    [Range(0f,1f)]
    [SerializeField] private float _hatChance;
    
    private CharactersData _charactersData;
    private IScoreService _scoreService;

    [Inject]
    public void Construct(ICharacterService characterService, IScoreService scoreService, CharactersData charactersData)
    {
        characterService.AssignManager(this);
        _charactersData = charactersData;
        _scoreService = scoreService;

        _scoreService.ActionCardPlayed += OnActionCardPlayed;
    }

    public void OnDestroy()
    {
        _scoreService.ActionCardPlayed -= OnActionCardPlayed;
    }

    private void OnActionCardPlayed(List<HumorType> humorTypes)
    {
        var reactionScore = _characters.Sum(character => character.ReactToCard(humorTypes));
        var normalizedScore = reactionScore / (20 * humorTypes.Count);
        _scoreService.SetReactionScore(normalizedScore);
    }

    public List<CharacterHumor> GetPublicHumor()
    {
        return _characters.Select(character => character.GetHumor()).ToList();
    }
    
    private void Start()
    {
        foreach (var character in _characters)
        {
            var visualData = GetCharacterSpriteData();
            var hasHat = Random.Range(0f, 1f) <= _hatChance;
            character.Initialize(visualData, _charactersData.GetRandomHumor(), hasHat ? _charactersData.GetHatSprite() : null);
        }
    }

    private CharacterVisualData GetCharacterSpriteData()
    {
        var animalType = (CharacterType)Random.Range(0, 4);
        return _charactersData.GetCharacterVisualData(animalType);
    }
}