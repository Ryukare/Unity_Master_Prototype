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

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(EnableUpgrade);
        _button.interactable = false;
    }

    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += CheckIfCanBeEnabled;
    }
    
    private void OnDisable()
    {
        GameEvents.OnProgressValueChanged -= CheckIfCanBeEnabled;
    }
    
    private void EnableUpgrade()
    {
        if (_upgradeCount == 0)
        {
            GameEvents.InvokeOnFirstUpgradeDone();
        }
        _upgradeCount++;
        SetUpgradeName();
        
        if (_upgradeInfo.clickPowerIncrease > 0)
        {
            GameEvents.InvokeOnClickPowerIncrease(_upgradeInfo.clickPowerIncrease);
        }

        if (_upgradeInfo.progressPerSecondIncrease > 0)
        {
            GameEvents.InvokeOnProgressPerSecondIncrease(_upgradeInfo.progressPerSecondIncrease);
        }
        
        GameEvents.InvokeOnCostPayed(_upgradeInfo.cost);
    }

    private void CheckIfCanBeEnabled(int progress)
    {
        if (_upgradeInfo == null)
        {
            return;
        }
        if (progress < _upgradeInfo.cost)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }

    public void SetUpgradeInfo(UpgradeScriptableObject upgradeInfo)
    {
        _upgradeInfo = upgradeInfo;
        upgradeNameText.text = $"{upgradeInfo.upgradeName} | {_upgradeCount}";
        upgradeCostText.text = $"Cost: {upgradeInfo.cost.ToString()}";
        
        if (upgradeInfo.clickPowerIncrease == 0)
        {
            clickPowerValueText.gameObject.SetActive(false);
        }
        else
        {
            clickPowerValueText.text = $"Click Power +{upgradeInfo.clickPowerIncrease.ToString()}";
        }
        
        if (upgradeInfo.progressPerSecondIncrease == 0)
        {
            progressPerSecondValueText.gameObject.SetActive(false);
        }
        else
        {
            progressPerSecondValueText.text = $"Progress Per Second +{upgradeInfo.progressPerSecondIncrease.ToString()}";
        }
    }

    private void SetUpgradeName()
    {
        upgradeNameText.text = $"{_upgradeInfo.upgradeName} | {_upgradeCount}";
    }
}
