using System;

public static class GameEvents
{
    public static event Action<int> OnProgressIncrease;

    public static void InvokeOnProgressIncrease(int value)
    {
        OnProgressIncrease?.Invoke(value);
    }
    
    public static event Action<int> OnProgressValueChanged;

    public static void InvokeOnProgressValueChanged(int value)
    {
        OnProgressValueChanged?.Invoke(value);
    }
    
    public static event Action<int, int> OnClickPowerIncrease;

    public static void InvokeOnClickPowerIncrease(int upgradeValue, int cost)
    {
        OnClickPowerIncrease?.Invoke(upgradeValue, cost);
    }
    
    public static event Action<int> OnClickPowerValueChanged;

    public static void InvokeOnClickPowerValueChanged(int value)
    {
        OnClickPowerValueChanged?.Invoke(value);
    }
}
