using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private SpriteMask _mask;
    [SerializeField] private GameObject _hatObject;
    [SerializeField] private SpriteMask _hatMask;
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
        if(_mask!=null)
            _mask.sprite = _visualData._defaultCharacter;
        
        if (hatSprite != null)
        {
            PutOnTheHat(visualData, hatSprite);
        }
        else
        {
            _hatObject.SetActive(false);
        }
    }
    
    public float ReactToCard(JokeData jokeData)
    {
        var totalHumor = _humorPreferences.ComputeHumorReaction(jokeData);
        var reactionSprite = _reactionsModel.GetCorrespondingReaction(Mathf.RoundToInt(totalHumor));
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
        if(_hatMask!=null)
            _hatMask.sprite = hatSprite._sprite;
    }

    private IEnumerator AnimateReaction(Sprite reactionSprite)
    {
        var spriteRenderer = _reactionObject.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0.7f, 0);
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        spriteRenderer.sprite = reactionSprite;
        _reactionObject.transform.DOScale(0, 0.5f).From().SetEase(Ease.OutBack);
        yield return new WaitForSeconds(Random.Range(3f, 3.5f));
        spriteRenderer.DOFade(0, 0.5f).OnComplete(() => spriteRenderer.sprite = null);
        //spriteRenderer.transform.DOMoveY(1, 0.5f).SetRelative().SetEase(Ease.InBack);
    }
}