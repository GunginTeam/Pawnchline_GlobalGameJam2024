using System.Collections.Generic;

public class JokeData
{
    public List<HumorType> JokeHumor;
    public bool WithIrony;

    public JokeData(List<HumorType> humorTypes, bool withIrony)
    {
        JokeHumor = humorTypes;
        WithIrony = withIrony;
    }
}