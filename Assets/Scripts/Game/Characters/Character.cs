using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _renderer;
    
    private CharacterVisualData _visualData;
    
    public void Initialize(CharacterVisualData visualData)
    {
        _visualData = visualData;
        _renderer.sprite = _visualData._defaultCharacter;
    }
}