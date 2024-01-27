using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _hatObject;
    [SerializeField] private GameObject _reactionObject;

    private CharacterVisualData _visualData;
    private CharacterHumor _humorPreferences;
    private ReactionsModel _reactionsModel;

    [Inject]
    public void Construct(ReactionsModel reactionsModel)
    {
        _reactionsModel = reactionsModel;
    }
    
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

    public CharacterHumor GetHumor()
    {
        return _humorPreferences;
    }

    public int ReactToCard(List<HumorType> humorTypes)
    {
        var totalHumor = _humorPreferences.ComputeHumorReaction(humorTypes);
        var reactionSprite = _reactionsModel.GetCorrespondingReaction(totalHumor);
        StartCoroutine(AnimateReaction(reactionSprite));
        return totalHumor;
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

    private IEnumerator AnimateReaction(Sprite reactionSprite)
    {
        _reactionObject.GetComponent<SpriteRenderer>().sprite = reactionSprite;
        yield return new WaitForSeconds(5);
        _reactionObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}