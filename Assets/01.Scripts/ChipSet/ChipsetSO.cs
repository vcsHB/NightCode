using Core.Attribute;
using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    [CreateAssetMenu(fileName = "ChipsetSO", menuName = "SO/Chipset/ChipsetSO")]
    public class ChipsetSO : ScriptableObject
    {
        [ReadOnly]public ushort id;

        [Space]
        [Header("Add position from leftbottom of each slot")]
        public List<Vector2Int> chipsetSize;
        public Chipset chipsetPrefab;
    }
}
