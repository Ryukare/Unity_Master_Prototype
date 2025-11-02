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
        upgradeNameText.text = upgradeInfo.upgradeName;
        upgradeCostText.text = upgradeInfo.cost.ToString();
        upgradeValueText.text = upgradeInfo.clickPowerIncrease.ToString();
    }

    private void EnableUpgrade()
    {
        GameEvents.InvokeOnClickPowerIncrease(upgradeInfo.clickPowerIncrease);
    }
    
}
