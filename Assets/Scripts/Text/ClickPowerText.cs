using TMPro;
using UnityEngine;

namespace Text
{
    public class ClickPowerText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
    
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
    
        private void OnEnable()
        {
            GameEvents.OnClickPowerValueChanged += ChangeText;
        }
        private void OnDisable()
        {
            GameEvents.OnClickPowerValueChanged -= ChangeText;
        }

        private void ChangeText(int value)
        {
            _text.text = value.ToString();
        }
    }
}

