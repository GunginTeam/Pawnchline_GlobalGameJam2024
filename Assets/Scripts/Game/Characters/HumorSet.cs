using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/HumorSet", fileName = "HumorSet")]
public class HumorSet : ScriptableObject
{
    [SerializeField] private List<CharacterHumor> _characterHumors;

    public CharacterHumor GetRandomHumor()
    {
        return _characterHumors[Random.Range(0, _characterHumors.Count)];
    }
}