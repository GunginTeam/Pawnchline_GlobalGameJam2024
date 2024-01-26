using System.Collections.Generic;

public interface ICharacterService
{
    List<CharacterHumor> GetCharactersHumors();
    void AssignManager(CharactersManager manager);
}