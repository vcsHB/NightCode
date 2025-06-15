using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    [CreateAssetMenu(menuName = "SO/Chipset/InitializeSO")]
    public class ChipsetInitializeSO : ScriptableObject
    {
        public List<Vector2Int> openInventory;
        public List<ChipsetSO> containChipsets;
    }
}
