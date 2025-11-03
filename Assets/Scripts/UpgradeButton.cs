using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private TextMeshProUGUI upgradeValueText;
    private Button _button;
    
    [SerializeField] private UpgradeScriptableObject upgradeInfo;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(EnableUpgrade);
        _button.interactable = false;
        upgradeNameText.text = upgradeInfo.upgradeName;
        upgradeCostText.text = $"Cost: {upgradeInfo.cost.ToString()}";
        upgradeValueText.text = $"Click Power +{upgradeInfo.clickPowerIncrease.ToString()}";
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
        GameEvents.InvokeOnClickPowerIncrease(upgradeInfo.clickPowerIncrease, upgradeInfo.cost);
    }

    private void CheckIfCanBeEnable(int value)
    {
        if (value <= 0)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }
}
