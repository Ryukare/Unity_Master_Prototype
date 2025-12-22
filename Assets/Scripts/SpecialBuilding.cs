using System.Collections;
using TMPro;
using UnityEngine;

public class SpecialBuilding : Building
{
        [Header("Special Components")]
        [SerializeField] private TMP_Dropdown modeDropDown;
        private int _currentModeIndex;
        private int _awaitingModeIndex;
        private double _productionQuantityModifier;
        private double _productionTimeModifier;
        
        protected override void Awake()
        {
                base.Awake();
                modeDropDown.onValueChanged.AddListener(ChangeMode);
                modeDropDown.interactable = false;
                _productionQuantityModifier = 1;
                _productionTimeModifier = 1;
        }

        private void ChangeMode(int value)
        {
                _awaitingModeIndex = value;
        }
        
        private void ChangeModifiers()
        {
                switch (_currentModeIndex)
                {
                        case 0:
                                _productionQuantityModifier = 1;
                                _productionTimeModifier = 1;
                                break;
                        case 1:
                                _productionQuantityModifier = 0.25;
                                _productionTimeModifier = 0.5;
                                break;
                        case 2: 
                                _productionQuantityModifier = 4;
                                _productionTimeModifier = 2;
                                break;
                }
                UpdateBuildingText(_productionQuantityModifier);
        }
        
        protected override void BuyBuilding()
        {
                if (level == 0)
                {
                        StartCoroutine(EnableProduction());
                        modeDropDown.interactable = true;
                }
                level++;
                UpdateBuildingText(_productionQuantityModifier);
                GameEvents.InvokeOnProgressCostPayed(buildingInfo.progressCost);
        }
        
        protected override IEnumerator EnableProduction()
        {
                while (true)
                {
                        yield return new WaitForSeconds(1f);
                        UpdateProductionTime(_productionTimeModifier);
                        if (currentProductionTime == buildingInfo.productionTime * _productionTimeModifier)
                        {
                                currentProductionTime = 0;
                                slider.value = 0;
                                
                                GameEvents.InvokeOnProgressIncrease((int)(buildingInfo.progressPerProduction * level *
                                                                          _productionQuantityModifier));
                                _currentModeIndex = _awaitingModeIndex;
                                ChangeModifiers();
                                _remainingProductionTime = (int)(buildingInfo.productionTime * _productionTimeModifier);
                                productionTimeText.text = $"{(_remainingProductionTime/60).ToString()}:{(_remainingProductionTime%60).ToString()}";
                        }
                }
        }
}