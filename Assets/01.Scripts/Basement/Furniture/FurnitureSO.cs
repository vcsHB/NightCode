using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/FurnitureSO")]
    public class FurnitureSO : ScriptableObject
    {
        public int furnitureID;
        public string furnitureName;
        [TextArea(6, 20)]
        public string furnitureDescription;
        public Sprite icon;

        public Furniture furniturePrefab;
    }
}
