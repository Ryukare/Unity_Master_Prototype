using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private BuildingScriptableObject buildingInfo;
    
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingCostText;
    [SerializeField] private TextMeshProUGUI progressPerProductionText;
    [SerializeField] private TextMeshProUGUI productionTimeText;
    [SerializeField] private Button button;
    [SerializeField] private Slider slider;
    private int _currentProductionTime;
    private float _productionPercentage;
    private int _remainingProductionTime;
    private int _level;

    private void OnEnable()
    {
        GameEvents.OnProgressValueChanged += CheckIfCanBeBought;
    }

    private void OnDisable()
    {
        GameEvents.OnProgressValueChanged -= CheckIfCanBeBought;
    }
    
    private void Awake()
    {
        _remainingProductionTime = buildingInfo.productionTime;
        SetBuildingText();
        button.onClick.AddListener(BuyBuilding);
        button.interactable = false;
    }

    private void SetBuildingText()
    {
        buildingNameText.text = $"{buildingInfo.buildingName} | {_level}";
        buildingCostText.text = $"{(buildingInfo.progressCost).ToString()} Progress";
        progressPerProductionText.text = $"{(buildingInfo.progressPerProduction * _level).ToString()}";
        productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
    }

    private void SetBuildingData()
    {
        buildingNameText.text = $"{buildingInfo.buildingName} | {_level}";
        progressPerProductionText.text = $"{(buildingInfo.progressPerProduction * _level).ToString()}";
    }

    private void SetProductionTime()
    {
        _currentProductionTime++;
        _remainingProductionTime = buildingInfo.productionTime - _currentProductionTime;
        _productionPercentage = (float)_currentProductionTime / buildingInfo.productionTime;
        productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
        slider.value = _productionPercentage;
    }
    
    private void CheckIfCanBeBought(int progress)
    {
        button.interactable = progress >= buildingInfo.progressCost;
    }
    
    private void BuyBuilding()
    {
        if (_level == 0)
        {
            StartCoroutine(EnableProduction());
        }
        _level++;
        SetBuildingData();
        GameEvents.InvokeOnProgressCostPayed(buildingInfo.progressCost);
    }
    
    private IEnumerator EnableProduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SetProductionTime();
            if (_currentProductionTime == buildingInfo.productionTime)
            {
                _currentProductionTime = 0;
                _remainingProductionTime = buildingInfo.productionTime;
                productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
                slider.value = 0;
                GameEvents.InvokeOnProgressIncrease(buildingInfo.progressPerProduction * _level);
            }
        }
    }
}
