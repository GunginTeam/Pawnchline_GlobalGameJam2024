using System;
using System.Collections.Generic;

[Serializable]
public class CharacterHumor
{
    public List<Humor> Humors = new ();
    
    public int ComputeHumorReaction(JokeData jokeData)
    {
        var totalHumor = 0;
        foreach (var humor in jokeData.JokeHumor)
        {
            var humorScore = Humors.Find(item => item.Type == humor).Value;
            if (jokeData.WithIrony && humorScore < 0)
            {
                humorScore = 0;
            }

            totalHumor += humorScore;
        }

        return totalHumor;
    }
}