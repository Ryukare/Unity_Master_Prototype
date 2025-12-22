using Buttons;
using ScriptableObjects;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    [SerializeField] private GameObject upgradeButtonPrefab; 
    
    private InvestmentScriptableObject[] _upgradeList;
    private int _currentInvestmentIndex;

    private void Awake()
    {
        _upgradeList = Resources.LoadAll<InvestmentScriptableObject>("Investments");
        InstantiateInvestment();
    }

    private void OnEnable()
    {
        GameEvents.OnFirstUpgradeDone += InstantiateInvestment;
    }

    private void OnDisable()
    {
        GameEvents.OnFirstUpgradeDone -= InstantiateInvestment;
    }

    private void InstantiateInvestment()
    {
        if (_currentInvestmentIndex == _upgradeList.Length)
        {
            return;
        }
        var button = Instantiate(upgradeButtonPrefab, gameObject.transform).GetComponent<InvestmentButton>();
        button.SetUpgradeInfo(_upgradeList[_currentInvestmentIndex]);
        _currentInvestmentIndex++;
    }
}
