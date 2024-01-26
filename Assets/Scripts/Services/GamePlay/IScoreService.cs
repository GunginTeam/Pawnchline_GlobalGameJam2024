using System;
using System.Collections.Generic;

public interface IScoreService
{
    event Action<List<HumorType>> CardPlayed;
}