using System;
using System.Collections.Generic;

public class ScoreService : IScoreService
{
    public event Action<List<HumorType>> CardPlayed;
}
