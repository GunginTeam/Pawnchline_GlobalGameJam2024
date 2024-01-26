using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _hatObject;

    private CharacterVisualData _visualData;
    private CharacterHumor _humorPreferences;

    public void Initialize(CharacterVisualData visualData, CharacterHumor humor, HatSprite hatSprite)
    {
        _humorPreferences = humor;
        _visualData = visualData;
        _renderer.sprite = _visualData._defaultCharacter;
        if (hatSprite != null)
        {
            PutOnTheHat(visualData, hatSprite);
        }
    }

    private void PutOnTheHat(CharacterVisualData visualData, HatSprite hatSprite)
    {
        var position = visualData._hatPosition;
        if (_renderer.flipX)
        {
            position.x *= -1;
        }

        _hatObject.transform.localPosition = position;

        _hatObject.GetComponent<SpriteRenderer>().sprite = hatSprite._sprite;
    }
}

[Serializable]
public class Humor
{
    public HumorType Type;
    [Range(-2,2)]
    public int Value;
}


