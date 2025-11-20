using UnityEngine;



namespace ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "InvestmentScriptableObject", 
        menuName = "ScriptableObjects/Investment"
        )]
    public class InvestmentScriptableObject : ScriptableObject
    {
        public string investmentName;
        public int progressCost;
        public int clickPowerIncrease;
        public int progressPerSecondIncrease;
    }
}
