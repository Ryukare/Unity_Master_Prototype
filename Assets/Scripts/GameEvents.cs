using System;

public static class GameEvents
{
    
    //Events related to progress resource
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
    
    //Events related to upgrades
    public static event Action OnFirstUpgradeDone;
    public static void InvokeOnFirstUpgradeDone()
    {
        OnFirstUpgradeDone?.Invoke();
    }
    
    //Events related to paying costs 
    public static event Action<int> OnCostPayed;
    public static void InvokeOnCostPayed(int value)
    {
        OnCostPayed?.Invoke(value);
    }

    //Events related to Click Power
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
    
    //Events related to Progress Per Second
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
