using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    public class ChipsetInventoryInfo
    {
        public event Action<Vector2Int> onActiveSlot;
        public event Action onInsertChipset;

        public Vector2Int inventorySize;
        public bool[,] isSlotActivated;

        //Use by chipset class is more safe than using index
        public int[,] chipsetArray;
        public List<int> containChipsetIndex;

        private CharacterEnum _character;

        public CharacterEnum Character => _character;

        public ChipsetInventoryInfo(CharacterEnum character, Vector2Int size)
        {
            inventorySize = size;
            _character = character;
            chipsetArray = new int[size.x, size.y];
            isSlotActivated = new bool[size.x, size.y];
            containChipsetIndex = new List<int>();

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    chipsetArray[i, j] = -1;
                }
            }
        }

        /// <summary>
        /// Check and set chipset by position and chipset offset
        /// </summary>
        /// <param name="chipset"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool TrySetChipset(int chipsetIndex, Vector2Int position)
        {
            Chipset chipset = ChipsetManager.Instance.GetChipset(chipsetIndex);
            List<Vector2Int> positions = InventoryPositionConverter.GetChipsetOffsets(inventorySize, position, chipset);

            //Check Can Insert Chipset
            if (CanChipsetInsert(positions) == false) return false;

            //Set Item
            for (int i = 0; i < positions.Count; i++)
                chipsetArray[positions[i].x, positions[i].y] = chipsetIndex;

            containChipsetIndex.Add(chipsetIndex);
            onInsertChipset?.Invoke();
            return true;
        }

        public bool CanChipsetInsert(List<Vector2Int> positions)
        {
            for (int i = 0; i < positions.Count; i++)
                if (chipsetArray[positions[i].x, positions[i].y] != -1)
                    return false;

            return true;
        }

        public bool CanChipsetInsert(Chipset chipset, Vector2Int position)
        {
            List<Vector2Int> positions = InventoryPositionConverter.GetChipsetOffsets(inventorySize, position, chipset);

            for (int i = 0; i < positions.Count; i++)
            {
                if (chipsetArray[positions[i].x, positions[i].y] != -1 || isSlotActivated[positions[i].x, positions[i].y] == false)
                {
                    return false;
                }

            }

            return true;
        }

        public void RemoveChipset(Vector2Int position)
        {
            chipsetArray[position.x, position.y] = -1;
        }

        public void ActiveInventorySlot(Vector2Int position)
        {
            if (isSlotActivated[position.x, position.y]) return;

            onActiveSlot?.Invoke(position);
            isSlotActivated[position.x, position.y] = true;
        }

        public void RemoveChipset(int chipsetIndex)
        {
            for (int i = 0; i < chipsetArray.GetLength(0); i++)
            {
                for (int j = 0; j < chipsetArray.GetLength(1); j++)
                {
                    if (chipsetArray[i, j] == chipsetIndex)
                        chipsetArray[i, j] = -1;
                }
            }
            containChipsetIndex.Remove(chipsetIndex);
        }
    }
}
