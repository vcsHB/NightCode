using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/FurnitureSetSO")]
    public class FurnitureSetSO : ScriptableObject
    {
        public List<FurnitureSO> furnitureSet;

        public FurnitureSO GetFurniture(int furnitureId)
            => furnitureSet.Find(furniture => furniture.furnitureID == furnitureId);
    }
}
