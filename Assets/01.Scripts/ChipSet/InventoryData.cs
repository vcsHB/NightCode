using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.TextCore.Text;

namespace Chipset
{
    public class InventoryData
    {
        public Dictionary<int, (Vector2Int center, int rotate)> assignedChipsets = new();
        public ChipsetInventorySlot[,] slots;
        public Chipset[,] chipsets;
        public CharacterEnum character;

        public List<Vector2Int> openSlot;
    }
}
