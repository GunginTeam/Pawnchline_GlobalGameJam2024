using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HumorSet))]
public class HumorSetInspector: Editor
{
    private HumorSet _humorSet;
    private int _spicyValue;
    private int _darkValue;
    private int _observationValue;
    private int _surrealValue;
    private int _languageValue;
    
    private void OnEnable()
    {
        _humorSet = (HumorSet)target;
    }

    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Calculate Total Values"))
        {
            CalculateTotalValues();
        }
        
        GUILayout.Label("Spicy Amount: "+_spicyValue);
        GUILayout.Label("Dark Amount: "+_darkValue);
        GUILayout.Label("Observation Amount: "+_observationValue);
        GUILayout.Label("Surreal Amount: "+_surrealValue);
        GUILayout.Label("Language Amount: "+_languageValue);
    }

    private void CalculateTotalValues()
    {
        _spicyValue = 0;
        _darkValue = 0;
        _observationValue = 0;
        _surrealValue = 0;
        _languageValue = 0;
        foreach (var humor in _humorSet.GetHumorList())
        {
            _spicyValue += humor.Humors.Find(item => item.Type == HumorType.Spicy).Value;
            _darkValue += humor.Humors.Find(item => item.Type == HumorType.Dark).Value;
            _observationValue += humor.Humors.Find(item => item.Type == HumorType.Observational).Value;
            _surrealValue += humor.Humors.Find(item => item.Type == HumorType.Surrealism).Value;
            _languageValue += humor.Humors.Find(item => item.Type == HumorType.Language).Value;
        }
    }
}