using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "BuildingScriptableObject", 
        menuName = "ScriptableObjects/Building"
        )]
    public class BuildingScriptableObject : ScriptableObject
    {
        public string buildingName;
        public int progressCost;
        public int progressPerProduction;
        public int productionTime;
    }
}
