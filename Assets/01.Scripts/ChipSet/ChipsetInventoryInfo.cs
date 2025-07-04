using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Chipset
{
    public class ChipsetInventoryInfo
    {
        public event Action<Vector2Int> onActiveSlot;
        public event Action onInsertChipset;

        public Vector2Int inventorySize;
        public bool[,] isSlotActivated;
        public Chipset[,] chipsetArray;

        private CharacterEnum _character;

        public CharacterEnum Character => _character;

        public ChipsetInventoryInfo(CharacterEnum character,Vector2Int size)
        {
            inventorySize = size;
            _character = character;
            chipsetArray = new Chipset[size.x, size.y];
            isSlotActivated = new bool[size.x, size.y];
        }

        /// <summary>
        /// Check and set chipset by position and chipset offset
        /// </summary>
        /// <param name="chipset"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool TrySetChipset(Chipset chipset, Vector2Int position)
        {
            List<Vector2Int> positions = InventoryPositionConverter.GetChipsetOffset(inventorySize, position, chipset);

            //Check Can Insert Chipset
            if (CanChipsetInsert(chipset, positions) == false) return false;

            //Set Item
            for (int i = 0; i < positions.Count; i++)
                chipsetArray[positions[i].x, positions[i].y] = chipset;

            onInsertChipset?.Invoke();
            return true;
        }

        public bool CanChipsetInsert(Chipset chipset, List<Vector2Int> positions)
        {
            for (int i = 0; i < positions.Count; i++)
                if (chipsetArray[positions[i].x, positions[i].y] != null)
                    return false;

            return true;
        }

        public bool CanChipsetInsert(Chipset chipset, Vector2Int position)
        {
            List<Vector2Int> positions = InventoryPositionConverter.GetChipsetOffset(inventorySize, position, chipset);

            for (int i = 0; i < positions.Count; i++)
                if (chipsetArray[positions[i].x, positions[i].y] != null)
                    return false;

            return true;
        }

        public void RemoveChipset(Vector2Int position)
        {
            chipsetArray[position.x, position.y] = null;
        }

        public void ActiveInventorySlot(Vector2Int position)
        {
            if (isSlotActivated[position.x, position.y]) return;

            onActiveSlot?.Invoke(position);
            isSlotActivated[position.x, position.y] = true;
        }
    }
}
