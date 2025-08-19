using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    public static class InventoryPositionConverter
    {
        //To move selection what out of inventory
        public static List<Vector2Int> GetChipsetOffsets(Vector2Int inventorySize, Vector2Int originPosition, Chipset chipset )
        {
            List<Vector2Int> positions = chipset.GetOffsets().ConvertAll(offset => originPosition + offset);
            Vector2Int offset = GetChipsetOffset(inventorySize, positions);

            //Apply Offset
            for(int i = 0; i < positions.Count; i++)
                positions[i] -= offset;

            return positions;
        }

        public static Vector2Int GetChipsetOffset(Vector2Int inventorySize, List<Vector2Int> positions)
        {
            Vector2Int offset = Vector2Int.zero;
            bool isValid = true;

            //Get Offset
            for (int i = 0; i < positions.Count; i++)
            {
                if (isValid == false) continue;

                if (positions[i].x < 0)
                {
                    if (offset.x > 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.x = Mathf.Min(offset.x, positions[i].x);
                }
                if (positions[i].y < 0)
                {
                    if (offset.y > 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.y = Mathf.Min(offset.y, positions[i].y);
                }
                if (positions[i].x >= inventorySize.x)
                {
                    if (offset.x < 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.x = Mathf.Max(offset.x, positions[i].x - (inventorySize.x - 1));
                }
                if (positions[i].y >= inventorySize.y)
                {
                    if (offset.y < 0)
                    {
                        isValid = false;
                        continue;
                    }
                    offset.y = Mathf.Max(offset.y, positions[i].y - (inventorySize.y - 1));
                }
            }

            return offset;
        }
    
        public static Vector2 GetALocalPositionAtBPosition(RectTransform ARectTransform, RectTransform BRectTransform)
        {
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, ARectTransform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(BRectTransform, screenPosition, Camera.main, out Vector2 canvasPosition);

            return canvasPosition;
        }
    }
}
