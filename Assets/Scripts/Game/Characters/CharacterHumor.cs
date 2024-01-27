using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class CharacterHumor
{
    public List<Humor> Humors = new ();
    
    public int ComputeHumorReaction(JokeData jokeData)
    {
        foreach (var humor in jokeData.JokeHumor)
        {
            var humorScore = Humors.Find(item => item.Type == humor).Value;
        }
        return jokeData.JokeHumor.Sum(humor => Humors.Find(item => item.Type == humor).Value);
    }
}