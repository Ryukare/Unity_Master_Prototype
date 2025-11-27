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
    protected int _level;

    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += CheckIfCanBeBought;
    }

    private void OnDisable()
    {
        GameEvents.OnProgressValueChanged -= CheckIfCanBeBought;
    }
    
    protected virtual void Awake()
    {
        _remainingProductionTime = buildingInfo.productionTime;
        SetBuildingText();
        button.onClick.AddListener(BuyBuilding);
        button.interactable = false;
    }

    private void SetBuildingText()
    {
        buildingNameText.text = $"{buildingInfo.buildingName}";
        buildingCostText.text = $"{(buildingInfo.progressCost).ToString()} Progress";
        progressPerProductionText.text = $"{(buildingInfo.progressPerProduction * _level).ToString()}";
        productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
    }

    protected void UpdateBuildingText(double modifier)
    {
        levelText.text = $"{_level}";
        progressPerProductionText.text = $"{(buildingInfo.progressPerProduction * _level * modifier).ToString()}";
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
        button.interactable = progress >= buildingInfo.progressCost;
    }
    
    protected virtual void BuyBuilding()
    {
        if (_level == 0)
        {
            StartCoroutine(EnableProduction());
        }
        _level++;
        UpdateBuildingText(1);
        GameEvents.InvokeOnProgressCostPayed(buildingInfo.progressCost);
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
                GameEvents.InvokeOnProgressIncrease(buildingInfo.progressPerProduction * _level);
            }
        }
    }
}
