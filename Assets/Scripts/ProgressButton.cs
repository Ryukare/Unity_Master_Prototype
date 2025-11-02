using UnityEngine;
using UnityEngine.UI;

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
        GameEvents.InvokeOnProgressIncrease(1);
    }
}
