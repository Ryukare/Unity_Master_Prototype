using TMPro;
using UnityEngine;

namespace Text
{
    public class ProgressPerSecondText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
    
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
    
        private void OnEnable()
        {
            GameEvents.OnProgressPerSecondValueChanged += ChangeText;
        }
        private void OnDisable()
        {
            GameEvents.OnProgressPerSecondValueChanged -= ChangeText;
        }

        private void ChangeText(int value)
        {
            _text.text = value.ToString();
        }
    }
}


