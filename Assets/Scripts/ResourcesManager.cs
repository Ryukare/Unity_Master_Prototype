using System;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private int _progress;

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
    }
    private void OnDisable()
    {
        GameEvents.OnProgressIncrease -= IncreaseProgress;
    }
    
    private void IncreaseProgress(int value)
    {
        Progress += value;
        Debug.Log(_progress);
    }
}
