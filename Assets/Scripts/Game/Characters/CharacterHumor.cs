using System;
using System.Collections.Generic;

[Serializable]
public class CharacterHumor
{
    public List<Humor> Humors = new ();
    
    public float ComputeHumorReaction(JokeData jokeData)
    {
        var totalHumor = 0;
        var jokesAffected = 0;
        foreach (var humor in jokeData.JokeHumor)
        {
            var humorScore = Humors.Find(item => item.Type == humor).Value;
            if (humorScore != 0)
            {
                jokesAffected++;
            }
            if (jokeData.WithIrony && humorScore < 0)
            {
                humorScore = 0;
            }
            
            totalHumor += humorScore;
        }

        return jokesAffected != 0?(float)totalHumor/(jokesAffected):0;
    }
}