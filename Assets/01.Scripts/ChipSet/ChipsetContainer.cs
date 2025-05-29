using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour
    {
        public List<ChipsetSO> containChipset;
        public ChipsetInventory inventory;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {

        }
    }
}
