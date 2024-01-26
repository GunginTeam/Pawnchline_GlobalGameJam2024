using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/CharactersSprites", fileName = "CharactersSprites")]
public class CharactersSprites : ScriptableObject
{
    [SerializeField] 
    private List<CharacterVisualData> _characterSprites;
    [SerializeField] 
    private List<HatSprite> _hatSprites;
}