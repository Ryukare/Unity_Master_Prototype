using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMultiplierToggleGroup : MonoBehaviour
{
    [SerializeField] private Toggle firstToggle;
    [SerializeField] private Toggle secondToggle;
    [SerializeField] private Toggle thirdToggle;

    private void Awake()
    {
        firstToggle.onValueChanged.AddListener(ActivateX1Multiplier);
        secondToggle.onValueChanged.AddListener(ActivateX10Multiplier);
        thirdToggle.onValueChanged.AddListener(ActivateX100Multiplier);
    }

    private void ActivateX1Multiplier(bool value)
    {
        if (!value)
        {
            return;
        }
        GameEvents.InvokeOnMultiplierChanged(1);
    }
    private void ActivateX10Multiplier(bool value)
    {
        if (!value)
        {
            return;
        }
        GameEvents.InvokeOnMultiplierChanged(10);
    }
    
    private void ActivateX100Multiplier(bool value)
    {
        if (!value)
        {
            return;
        }
        GameEvents.InvokeOnMultiplierChanged(100);
    }
}
