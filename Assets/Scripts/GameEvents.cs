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
    
    public static event Action<int> OnCostPayed;

    public static void InvokeOnCostPayed(int value)
    {
        OnCostPayed?.Invoke(value);
    }
    
    public static event Action<int> OnClickPowerIncrease;

    public static void InvokeOnClickPowerIncrease(int value)
    {
        OnClickPowerIncrease?.Invoke(value);
    }
    
    public static event Action<int> OnClickPowerValueChanged;

    public static void InvokeOnClickPowerValueChanged(int value)
    {
        OnClickPowerValueChanged?.Invoke(value);
    }
    
    public static event Action< int> OnProgressPerSecondIncrease;

    public static void InvokeOnProgressPerSecondIncrease(int value)
    {
        OnProgressPerSecondIncrease?.Invoke(value);
    }
    
    public static event Action<int> OnProgressPerSecondValueChanged;

    public static void InvokeOnProgressPerSecondValueChanged(int value)
    {
        OnProgressPerSecondValueChanged?.Invoke(value);
    }
    
    
    
}
