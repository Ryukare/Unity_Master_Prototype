
public class ResourcesManager : Singleton<ResourcesManager>
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

    public int ClickPower
    {
        get => _clickPower;
        set
        {
            _clickPower = value; 
            GameEvents.InvokeOnClickPowerValueChanged(value);
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
    
    private void IncreaseClickPower(int upgradeValue, int cost)
    {
        ClickPower += upgradeValue;
        Progress -= cost;
    }
}
