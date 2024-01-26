using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GunginJokes/CharactersSprites", fileName = "CharactersSprites")]
public class CharactersData : ScriptableObject
{
    [SerializeField] private List<CharacterVisualData> _characterSprites;
    [SerializeField] private List<HatSprite> _hatSprites;
    [SerializeField] private HumorSet _humorSet;

    public CharacterVisualData GetCharacterVisualData(CharacterType type)
    {
        return _characterSprites.Find(item => item._type == type);
    }

    public HatSprite GetHatSprite()
    {
        return _hatSprites[Random.Range(0, _hatSprites.Count)];
    }

    public CharacterHumor GetRandomHumor()
    {
        return _humorSet.GetRandomHumor();
    }
}