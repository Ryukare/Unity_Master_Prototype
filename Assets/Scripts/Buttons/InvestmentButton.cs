using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Buttons
{
    public class InvestmentButton : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI upgradeNameText;
        [SerializeField] private TextMeshProUGUI upgradeCostText;
        [SerializeField] private TextMeshProUGUI clickPowerValueText;
        [SerializeField] private TextMeshProUGUI progressPerSecondValueText;
        
        private Button _button;
        private InvestmentScriptableObject _investmentInfo;

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
            
            if (_investmentInfo.clickPowerIncrease > 0)
            {
                GameEvents.InvokeOnClickPowerChanged(_investmentInfo.clickPowerIncrease * _multiplier);
            }

            if (_investmentInfo.progressPerSecondIncrease > 0)
            {
                GameEvents.InvokeOnProgressPerSecondChanged(_investmentInfo.progressPerSecondIncrease * _multiplier);
            }

            GameEvents.InvokeOnProgressCostPayed(_investmentInfo.progressCost * _multiplier);
        }
        
        private void CheckIfCanBeEnabled(int progress)
        {
            if (_investmentInfo == null)
            {
                return;
            }
            _button.interactable = progress >= _investmentInfo.progressCost * _multiplier;
        }

        public void SetUpgradeInfo(InvestmentScriptableObject investmentInfo)
        {
            _investmentInfo = investmentInfo;
            SetUpgradeText();
        }

        private void SetUpgradeText()
        {
            upgradeNameText.text = $"{_investmentInfo.investmentName} | {_upgradeCount}";
            upgradeCostText.text = $"Cost: {(_investmentInfo.progressCost * _multiplier).ToString()}";
            
            if (_investmentInfo.clickPowerIncrease == 0)
            {
                clickPowerValueText.gameObject.SetActive(false);
            }
            else
            {
                clickPowerValueText.text = $"Click Power +{(_investmentInfo.clickPowerIncrease * _multiplier).ToString()}";
            }
            
            if (_investmentInfo.progressPerSecondIncrease == 0)
            {
                progressPerSecondValueText.gameObject.SetActive(false);
            }
            else
            {
                progressPerSecondValueText.text = $"Progress Per Second +{(_investmentInfo.progressPerSecondIncrease * _multiplier).ToString()}";
            }
        }
        
        private void SetUpgradeName()
        {
            upgradeNameText.text = $"{_investmentInfo.investmentName} | {_upgradeCount}";
        }

        private void SetMultiplierValues(int newMultiplier)
        {
            _multiplier = newMultiplier;
            SetUpgradeText();
            CheckIfCanBeEnabled(ResourcesManager.Instance.Progress);
        }
    }
}
