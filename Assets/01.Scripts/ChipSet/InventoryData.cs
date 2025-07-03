using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    public class InventoryData
    {
        public event Action onSetChipset;
        public event Action onRemoveChipset;
        public event Action onRefreshOpenInventory;

        public Dictionary<int, (Vector2Int center, int rotate)> assignedChipsets = new();
        public ChipsetInventorySlot[,] slots;
        public Chipset[,] chipsets;
        public RectTransform chipsetParentRect;
        public CharacterEnum character;

        public List<Vector2Int> openSlot;

        public InventoryData(CharacterEnum character, Vector2Int inventorySize, List<Vector2Int> openSlot, RectTransform chipsetParent)
        {
            this.openSlot = openSlot;
            this.character = character;
            assignedChipsets = new Dictionary<int, (Vector2Int center, int rotate)>();
            slots = new ChipsetInventorySlot[inventorySize.x, inventorySize.y];
            chipsets = new Chipset[inventorySize.x, inventorySize.y];
            chipsetParentRect = chipsetParent;
        }

        public void SetChipset(Vector2Int selectedPosition, int chipsetIndex, Chipset chipset)
        {
            onSetChipset?.Invoke();
            chipset.GetOffsets().ForEach(offset =>
            {
                Vector2Int position = selectedPosition + offset;
                chipsets[position.x, position.y] = chipset;
            });

            if (assignedChipsets.ContainsKey(chipsetIndex))
                assignedChipsets.Remove(chipsetIndex);

            Vector2Int center = selectedPosition - chipset.GetSelectOffset();
            Vector2 localPosition = TransformSlotPositionToCanvasPosition(center);
            chipset.SetPosition(localPosition);
            assignedChipsets.Add(chipsetIndex, (center, chipset.Rotation));
        }

        public void RemoveChipset(int chipsetIndex)
        {
            onRemoveChipset?.Invoke();

            if (assignedChipsets.ContainsKey(chipsetIndex))
                assignedChipsets.Remove(chipsetIndex);
        }

        public void OpenInventory(Vector2Int position)
        {
            onRefreshOpenInventory?.Invoke();
            openSlot.Add(position);
        }

        private Vector2 TransformSlotPositionToCanvasPosition(Vector2Int slotPosition)
        {
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint
                (Camera.main, slots[slotPosition.x, slotPosition.y].RectTrm.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(chipsetParentRect, screenPosition, Camera.main, out Vector2 canvasPosition);

            return canvasPosition;
        }
    }
}