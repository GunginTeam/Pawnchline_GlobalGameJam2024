using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharactersManager : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private List<Transform> _characterPositions;

    private IInstancer _instancer;
    private CharactersSpritesModel _charactersSpritesModel;

    [Inject]
    public void Construct(IInstancer instancer, CharactersSpritesModel charactersSpritesModel)
    {
        _instancer = instancer;
        _charactersSpritesModel = charactersSpritesModel;
    }

    private void Start()
    {
        foreach (var position in _characterPositions)
        {
            var visualData = GetCharacterSpriteData();
            _instancer.Create<Character>(_characterPrefab.gameObject, position).Initialize(visualData);
        }
    }

    private CharacterVisualData GetCharacterSpriteData()
    {
        var animalType = (CharacterType)Random.Range(0, 5);
        return _charactersSpritesModel.GetCharacterVisualData(animalType);
    }
}