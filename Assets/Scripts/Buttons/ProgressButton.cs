using UnityEngine;
using UnityEngine.UI;


namespace Buttons
{
    public class ProgressButton : MonoBehaviour
    {
        private Button _button;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(IncreaseProgress);
        }

        private void IncreaseProgress()
        {
            GameEvents.InvokeOnProgressIncreaseByClicking();
        }
    }
}

