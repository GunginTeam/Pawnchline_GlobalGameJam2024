using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class CharacterHumor
{
    public List<Humor> Humors = new ();
    
    public int ComputeHumorReaction(List<HumorType> humorTypes)
    {
        return humorTypes.Sum(humor => Humors.Find(item => item.Type == humor).Value);
    }
}