using System;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private int _progress;
    private int _clickPower = 1;
    
    public int Progress
    {
        get => _progress;
        set
        {
            _progress = value; 
            GameEvents.InvokeOnProgressValueChanged(value);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnProgressIncrease += IncreaseProgress;
        GameEvents.OnClickPowerIncrease += IncreaseClickPower;
    }
    private void OnDisable()
    {
        GameEvents.OnProgressIncrease -= IncreaseProgress;
    }
    
    private void IncreaseProgress(int value)
    {
        Progress += value * _clickPower;
    }
    
    private void IncreaseClickPower(int value)
    {
        _clickPower += value;
        Debug.Log($"Click Power: {_clickPower}");
    }
}
