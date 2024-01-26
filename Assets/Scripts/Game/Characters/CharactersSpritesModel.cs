using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/CharactersSprites", fileName = "CharactersSprites")]
public class CharactersSpritesModel : ScriptableObject
{
    [SerializeField] 
    private List<CharacterVisualData> _characterSprites;
    [SerializeField] 
    private List<HatSprite> _hatSprites;

    public CharacterVisualData GetCharacterVisualData(CharacterType type)
    {
        return _characterSprites.Find(item => item.Type == type);
    }
}