
using System.Collections;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    private int _progress;
    private int _clickPower = 1;
    private int _progressPerSecond;
    private int _multiplier;
    
    public int Progress
    {
        get => _progress;
        set
        {
            _progress = value; 
            GameEvents.InvokeOnProgressValueChanged(value);
        }
    }

    public int ClickPower
    {
        get => _clickPower;
        set
        {
            _clickPower = value; 
            GameEvents.InvokeOnClickPowerValueChanged(value);
        }
    }

    public int ProgressPerSecond
    {
        get => _progressPerSecond;
        set
        {
            _progressPerSecond = value; 
            GameEvents.InvokeOnProgressPerSecondValueChanged(value);
        }
    }

    public int Multiplier => _multiplier;

    private void OnEnable()
    {
        GameEvents.OnProgressIncrease += IncreaseProgress;
        GameEvents.OnClickPowerChanged += IncreaseClickPower;
        GameEvents.OnProgressPerSecondChanged += IncreaseProgressPerSecond;
        GameEvents.OnProgressCostPayed += DecreaseProgress;
        GameEvents.OnMultiplierChanged += ChangeMultiplier;
    }
    private void OnDisable()
    {
        GameEvents.OnProgressIncrease -= IncreaseProgress;
        GameEvents.OnClickPowerChanged -= IncreaseClickPower;
        GameEvents.OnProgressPerSecondChanged -= IncreaseProgressPerSecond;
        GameEvents.OnProgressCostPayed -= DecreaseProgress;
        GameEvents.OnMultiplierChanged -= ChangeMultiplier;
    }

    private void Start()
    {
        StartCoroutine(ProduceProgress());
    }

    private IEnumerator ProduceProgress()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Progress += _progressPerSecond;
        }
    }
    
    private void IncreaseProgress(int value)
    {
        Progress += value * _clickPower;
    }

    private void DecreaseProgress(int value)
    {
        Progress -= value;
    }
    
    private void IncreaseClickPower(int value)
    {
        ClickPower += value;
    }
    
    private void IncreaseProgressPerSecond(int value)
    {
        ProgressPerSecond += value;
    }

    private void ChangeMultiplier(int value)
    {
        _multiplier = value;
    }
}
