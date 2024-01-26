using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterVisualData _visualData;
    
    public void Initialize(CharacterVisualData visualData)
    {
        _visualData = visualData;
    }
}