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
    
    //Events related to changing upgrade multiplier
    public static event Action<int> OnMultiplierChanged;
    public static void InvokeOnMultiplierChanged(int value)
    {
        OnMultiplierChanged?.Invoke(value);
    }
    
    //Events related to upgrades
    public static event Action OnFirstUpgradeDone;
    public static void InvokeOnFirstUpgradeDone()
    {
        OnFirstUpgradeDone?.Invoke();
    }
    
    //Events related to paying progress cost
    public static event Action<int> OnProgressCostPayed;
    public static void InvokeOnProgressCostPayed(int value)
    {
        OnProgressCostPayed?.Invoke(value);
    }

    //Events related to Click Power
    public static event Action<int> OnClickPowerValueChanged;
    public static void InvokeOnClickPowerValueChanged(int value)
    {
        OnClickPowerValueChanged?.Invoke(value);
    }
    
    public static event Action<int> OnClickPowerChanged;
    public static void InvokeOnClickPowerChanged(int value)
    {
        OnClickPowerChanged?.Invoke(value);
    }
    
    
    //Events related to Progress Per Second
    public static event Action<int> OnProgressPerSecondValueChanged;
    public static void InvokeOnProgressPerSecondValueChanged(int value)
    {
        OnProgressPerSecondValueChanged?.Invoke(value);
    }
    public static event Action<int> OnProgressPerSecondChanged;
    public static void InvokeOnProgressPerSecondChanged(int value)
    {
        OnProgressPerSecondChanged?.Invoke(value);
    }
}
