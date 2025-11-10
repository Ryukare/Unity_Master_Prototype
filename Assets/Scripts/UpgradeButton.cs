using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI clickPowerValueText;
    [SerializeField] private TextMeshProUGUI progressPerSecondValueText;
    
    private Button _button;
    private UpgradeScriptableObject _upgradeInfo;

    private int _upgradeCount;
    private int _multiplier;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TriggerUpgrade);
        _button.interactable = false;
        _multiplier = ResourcesManager.Instance.Multiplier;
        if (_multiplier == 0)
        {
            _multiplier = 1;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += CheckIfCanBeEnabled;
        GameEvents.OnMultiplierChanged += SetMultiplierValues;
    }
    
    private void OnDisable()
    {
        GameEvents.OnProgressValueChanged -= CheckIfCanBeEnabled;
        GameEvents.OnMultiplierChanged -= SetMultiplierValues;
    }
    
    private void TriggerUpgrade()
    {
        if (_upgradeCount == 0)
        {
            GameEvents.InvokeOnFirstUpgradeDone();
        }
        _upgradeCount += _multiplier;
        SetUpgradeName();
        
        if (_upgradeInfo.clickPowerIncrease > 0)
        {
            GameEvents.InvokeOnClickPowerChanged(_upgradeInfo.clickPowerIncrease * _multiplier);
        }

        if (_upgradeInfo.progressPerSecondIncrease > 0)
        {
            GameEvents.InvokeOnProgressPerSecondChanged(_upgradeInfo.progressPerSecondIncrease * _multiplier);
        }

        GameEvents.InvokeOnProgressCostPayed(_upgradeInfo.progressCost * _multiplier);
    }
    
    private void CheckIfCanBeEnabled(int progress)
    {
        if (_upgradeInfo == null)
        {
            return;
        }
        _button.interactable = progress >= _upgradeInfo.progressCost * _multiplier;
    }

    public void SetUpgradeInfo(UpgradeScriptableObject upgradeInfo)
    {
        _upgradeInfo = upgradeInfo;
        SetUpgradeText();
    }

    private void SetUpgradeText()
    {
        upgradeNameText.text = $"{_upgradeInfo.upgradeName} | {_upgradeCount}";
        upgradeCostText.text = $"Cost: {(_upgradeInfo.progressCost * _multiplier).ToString()}";
        
        if (_upgradeInfo.clickPowerIncrease == 0)
        {
            clickPowerValueText.gameObject.SetActive(false);
        }
        else
        {
            clickPowerValueText.text = $"Click Power +{(_upgradeInfo.clickPowerIncrease * _multiplier).ToString()}";
        }
        
        if (_upgradeInfo.progressPerSecondIncrease == 0)
        {
            progressPerSecondValueText.gameObject.SetActive(false);
        }
        else
        {
            progressPerSecondValueText.text = $"Progress Per Second +{(_upgradeInfo.progressPerSecondIncrease * _multiplier).ToString()}";
        }
    }
    
    private void SetUpgradeName()
    {
        upgradeNameText.text = $"{_upgradeInfo.upgradeName} | {_upgradeCount}";
    }

    private void SetMultiplierValues(int newMultiplier)
    {
        _multiplier = newMultiplier;
        SetUpgradeText();
        CheckIfCanBeEnabled(ResourcesManager.Instance.Progress);
    }
}
