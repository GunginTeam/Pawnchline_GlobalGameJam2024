using System.Collections.Generic;
using UnityEngine;

public class CharacterService : ICharacterService
{
    private CharactersManager _manager;
    public List<CharacterHumor> GetCharactersHumors()
    {
        return _manager.GetPublicHumor();
    }

    public void AssignManager(CharactersManager manager)
    {
        if(_manager!=null)
            Debug.LogError("THERE IS ALREADY A CHARACTER MANAGER YOU IDIOT!");
        
        _manager = manager;
    }
}