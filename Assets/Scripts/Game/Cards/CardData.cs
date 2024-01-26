using System;
using System.Collections.Generic;

[Serializable]
public class CardData
{
    public string TextKey;

    public List<HumorType> HummorTypes = new();
}