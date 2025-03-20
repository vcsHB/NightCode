using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Basement/Cafe/FoodSO")]
    public class FoodSO : ScriptableObject
    {
        public string foodName;
        public Sprite icon;
        public int cost;
    }
}
