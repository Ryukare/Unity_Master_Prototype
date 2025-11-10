using UnityEngine;

[CreateAssetMenu(
    fileName = "UpgradeScriptableObject", 
    menuName = "Upgrade", 
    order = 0)]
public class UpgradeScriptableObject : ScriptableObject
{
    public string upgradeName;
    public int progressCost;
    public int clickPowerIncrease;
    public int progressPerSecondIncrease;
}
