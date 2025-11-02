using TMPro;
using UnityEngine;

public class ProgressText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    
    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += ChangeText;
    }
    private void OnDisable()
    {
        GameEvents.OnProgressIncrease -= ChangeText;
    }

    private void ChangeText(int value)
    {
        _text.text = value.ToString();
    }
}
