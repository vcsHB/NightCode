using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    [CreateAssetMenu(fileName = "ChipsetGruopSO", menuName = "SO/Chipset/ChipsetGruopSO")]
    public class ChipsetGroupSO : ScriptableObject
    {
        public List<ChipsetSO> chipsetGroup;

        public ChipsetSO GetChipset(ushort id)
        {
            if (id >= chipsetGroup.Count)
            {
                Debug.LogError($"Chipset with id {id} does not exist in the group.");
                return null;
            }
            return chipsetGroup[id];
        }

        private void OnValidate()
        {
            for (ushort i = 0; i < chipsetGroup.Count; i++)
                chipsetGroup[i].id = i;
        }
    }
}
