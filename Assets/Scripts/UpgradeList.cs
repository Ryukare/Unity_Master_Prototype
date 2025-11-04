using System;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    [SerializeField] private GameObject upgradeButtonPrefab; 
    
    private UpgradeScriptableObject[] _upgradeList;
    private int _currentUpgradeIndex;

    private void Awake()
    {
        _upgradeList = Resources.LoadAll<UpgradeScriptableObject>("Upgrades");
        InstantiateUpgrade();
    }

    private void OnEnable()
    {
        GameEvents.OnFirstUpgradeDone += InstantiateUpgrade;
    }

    private void OnDisable()
    {
        GameEvents.OnFirstUpgradeDone -= InstantiateUpgrade;
    }

    private void InstantiateUpgrade()
    {
        if (_currentUpgradeIndex == _upgradeList.Length)
        {
            return;
        }
        var button = Instantiate(upgradeButtonPrefab, gameObject.transform).GetComponent<UpgradeButton>();
        button.SetUpgradeInfo(_upgradeList[_currentUpgradeIndex]);
        _currentUpgradeIndex++;
    }
}
