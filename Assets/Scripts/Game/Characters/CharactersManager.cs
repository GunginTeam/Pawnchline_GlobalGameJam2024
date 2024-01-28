using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
    public void Construct(IScoreService scoreService, CharactersData charactersData)
    {
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
        _scoreService.SetReactionScore(normalizedScore);
    }
    
    private void Start()
    {
        foreach (var character in _characters)
        {
            var visualData = GetCharacterSpriteData();
            var hasHat = Random.Range(0f, 1f) <= _hatChance;
            character.Initialize(visualData, _charactersData.GetRandomHumor(), hasHat ? _charactersData.GetHatSprite() : null);
            character.transform.DOScaleY(0, 0);
        }

        StartCoroutine(DisplayCharacters());
    }

    private IEnumerator DisplayCharacters()
    {
        foreach (var character in _characters)
        {
            character.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.15f);
        }
    }

    private CharacterVisualData GetCharacterSpriteData()
    {
        var animalType = (CharacterType)Random.Range(0, _animalTypeAmount);
        return _charactersData.GetCharacterVisualData(animalType);
    }
}