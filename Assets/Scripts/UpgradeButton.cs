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
    
    [SerializeField] private UpgradeScriptableObject upgradeInfo;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(EnableUpgrade);
        _button.interactable = false;
        upgradeNameText.text = upgradeInfo.upgradeName;
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

    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += CheckIfCanBeEnable;
    }
    
    private void OnDisable()
    {
        GameEvents.OnProgressValueChanged -= CheckIfCanBeEnable;
    }
    
    private void EnableUpgrade()
    {
        if (upgradeInfo.clickPowerIncrease > 0)
        {
            GameEvents.InvokeOnClickPowerIncrease(upgradeInfo.clickPowerIncrease);
        }

        if (upgradeInfo.progressPerSecondIncrease > 0)
        {
            GameEvents.InvokeOnProgressPerSecondIncrease(upgradeInfo.progressPerSecondIncrease);
        }
        
        GameEvents.InvokeOnCostPayed(upgradeInfo.cost);
    }

    private void CheckIfCanBeEnable(int progress)
    {
        if (progress < upgradeInfo.cost)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }
}
