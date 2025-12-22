using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] protected BuildingScriptableObject buildingInfo;
    
    [Header("Components")]
    [SerializeField] protected TextMeshProUGUI buildingNameText;
    [SerializeField] protected TextMeshProUGUI buildingCostText;
    [SerializeField] protected TextMeshProUGUI progressPerProductionText;
    [SerializeField] protected TextMeshProUGUI productionTimeText;
    [SerializeField] protected TextMeshProUGUI levelText;
    [SerializeField] protected Button button;
    [SerializeField] protected Slider slider;
    
    protected int currentProductionTime;
    protected float _productionPercentage;
    protected int _remainingProductionTime;
    protected int level;
    private int _multiplier;

    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += CheckIfCanBeBought;
        GameEvents.OnMultiplierChanged += SetMultiplierValues;
    }

    private void OnDisable()
    {
        GameEvents.OnProgressValueChanged -= CheckIfCanBeBought;
        GameEvents.OnMultiplierChanged -= SetMultiplierValues;
    }
    
    protected virtual void Awake()
    {
        _remainingProductionTime = buildingInfo.productionTime;
        button.onClick.AddListener(BuyBuilding);
        button.interactable = false;
        _multiplier = ResourcesManager.Instance.Multiplier;
        if (_multiplier == 0)
        {
            _multiplier = 1;
        }
        SetBuildingText();
    }

    private void SetBuildingText()
    {
        buildingNameText.text = $"{buildingInfo.buildingName}";
        SetCostText();
        progressPerProductionText.text = $"{(buildingInfo.progressPerProduction * level).ToString()}";
        productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
    }

    private void SetCostText()
    {
        buildingCostText.text = $"{(buildingInfo.progressCost * _multiplier).ToString()} Progress";
    }
    
    protected void UpdateBuildingText(double modifier)
    {
        levelText.text = $"{level}";
        progressPerProductionText.text = $"{(buildingInfo.progressPerProduction * level * modifier).ToString()}";
    }

    protected void UpdateProductionTime(double modifier)
    {
        currentProductionTime++;
        _remainingProductionTime = (int)(buildingInfo.productionTime * modifier) - currentProductionTime;
        _productionPercentage = currentProductionTime / (float)(buildingInfo.productionTime * modifier);
        productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
        slider.value = _productionPercentage;
    }
    
    private void CheckIfCanBeBought(int progress)
    {
        button.interactable = progress >= buildingInfo.progressCost * _multiplier;;
    }
    
    protected virtual void BuyBuilding()
    {
        if (level == 0)
        {
            StartCoroutine(EnableProduction());
        }
        level += 1 * _multiplier;;
        UpdateBuildingText(1);
        GameEvents.InvokeOnProgressCostPayed(buildingInfo.progressCost * _multiplier);
    }
    
    protected virtual IEnumerator EnableProduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            UpdateProductionTime(1);
            if (currentProductionTime == buildingInfo.productionTime)
            {
                currentProductionTime = 0;
                _remainingProductionTime = buildingInfo.productionTime;
                productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
                slider.value = 0;
                GameEvents.InvokeOnProgressIncrease(buildingInfo.progressPerProduction * level);
            }
        }
    }
    
    private void SetMultiplierValues(int newMultiplier)
    {
        _multiplier = newMultiplier;
        SetCostText();
        CheckIfCanBeBought(ResourcesManager.Instance.Progress);
    }
}
