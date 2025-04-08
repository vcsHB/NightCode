using System;
using UnityEngine;

namespace Office.Armory
{

    public class ArmoryPanel : MonoBehaviour
    {
        private WeaponSlot[] _slots;

        private void Awake()
        {
            _slots = GetComponentsInChildren<WeaponSlot>();
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OnSelectEvent += HandleSlotSelected;
            }
        }

        private void HandleSlotSelected()
        {
            
        }
    }

}
