using System;
using UnityEngine;
using UnityEngine.UI;

public enum Multiplier
{
    X1,
    X10,
    X100
}

public class UpgradeMultiplierToggle : MonoBehaviour
{
    private Toggle _toggle;
    [SerializeField] private Multiplier multiplier;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ActivateMultiplier);
    }

    private void ActivateMultiplier(bool value)
    {
        if (!value)
        {
            return;
        }

        switch (multiplier)
        {
            case Multiplier.X1:
                GameEvents.InvokeOnMultiplierChanged(1);
                break;
            case Multiplier.X10:
                GameEvents.InvokeOnMultiplierChanged(10);
                break;
            case Multiplier.X100:
                GameEvents.InvokeOnMultiplierChanged(100);
                break;
        }
    }
}
