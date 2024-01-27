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
    [SerializeField]
    private int _animalTypeAmount;
    
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

    private void OnActionCardPlayed(JokeData jokeData)
    {
        var reactionScore = _characters.Sum(character => character.ReactToCard(jokeData));
        var normalizedScore = reactionScore / 10;
        Debug.Log("Score: "+normalizedScore);
        _scoreService.SetReactionScore(normalizedScore);
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
        var animalType = (CharacterType)Random.Range(0, _animalTypeAmount);
        return _charactersData.GetCharacterVisualData(animalType);
    }
}