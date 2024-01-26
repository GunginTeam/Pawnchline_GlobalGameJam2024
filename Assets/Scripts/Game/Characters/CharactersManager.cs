using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CharactersManager : MonoBehaviour
{
    [FormerlySerializedAs("_characterPositions")] [SerializeField] private List<Character> _characters;
    
    private CharactersSpritesModel _charactersSpritesModel;

    [Inject]
    public void Construct(CharactersSpritesModel charactersSpritesModel)
    {
        _charactersSpritesModel = charactersSpritesModel;
    }

    private void Start()
    {
        foreach (var character in _characters)
        {
            var visualData = GetCharacterSpriteData();
            character.Initialize(visualData);
        }
    }

    private CharacterVisualData GetCharacterSpriteData()
    {
        var animalType = (CharacterType)Random.Range(0, 4);
        return _charactersSpritesModel.GetCharacterVisualData(animalType);
    }
}