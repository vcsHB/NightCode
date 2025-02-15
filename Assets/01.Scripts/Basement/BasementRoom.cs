using Basement.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public abstract class BasementRoom : MonoBehaviour
    {
        public FurnitureSetSO furnitureSet;
        public BasementRoomType roomType;

        [HideInInspector]
        public List<FurnitureInfo> furnitureInfo;

        public abstract void SetFactor(string factor);

        public virtual void SetFurniture()
        {
            furnitureInfo.ForEach(furniture =>
            {
                FurnitureSO furnitureSO = furnitureSet.GetFurniture(furniture.furnitureId);
                Furniture furnitureInstance = Instantiate(furnitureSO.furniturePrefab, furniture.furniturePosition, Quaternion.identity);
                //furnitureInstance.Init();
            });
        }
    }

    [SerializeField]
    public struct FurnitureInfo
    {
        public int furnitureId;
        public Vector2 furniturePosition;
    }
}
