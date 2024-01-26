using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunginJokes/ReactionData", fileName = "ReactionData")]
public class ReactionsModel : ScriptableObject
{
    [SerializeField] private List<ReactionData> ReactionList;

    public Sprite GetCorrespondingReaction(int reaction)
    {
        reaction = Math.Clamp(reaction, -2, 2);
        return ReactionList.Find(item => item.Value == reaction).Sprite;
    }
}