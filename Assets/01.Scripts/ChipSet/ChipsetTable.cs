using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chipset
{
    public class ChipsetTable : MonoBehaviour
    {
        public event Action<ChipsetInventory> onSelectInventory;

        public ChipsetGroupSO chipsetGroupSO;

        private Dictionary<CharacterEnum, ChipsetInventory> _inventory;
        private CharacterEnum _selectedCharacter;
        private List<Vector2Int> _openInventory;

        public CharacterEnum SelectedCharacter => _selectedCharacter;
        private RectTransform RectTrm => transform as RectTransform;
        
        public void Initialize(List<Vector2Int> openInventory, List<ChipsetData>[] chipsetDatas, List<Chipset> containChipset)
        {
            _openInventory = openInventory;
            _inventory = new Dictionary<CharacterEnum, ChipsetInventory>();

            var inventoryList = GetComponentsInChildren<ChipsetInventory>();

            for (int i = 0; i < inventoryList.Length; i++)
            {
                _inventory.Add((CharacterEnum)i, inventoryList[i]);
                inventoryList[i].Initialize((CharacterEnum)i, chipsetDatas[i], containChipset, _openInventory);
            }

            RectTrm.sizeDelta = new Vector2(RectTrm.sizeDelta.x, _inventory[CharacterEnum.An].RectTrm.rect.height);
            StartCoroutine(DelayInitializeInventory());
        }

        private IEnumerator DelayInitializeInventory()
        {
            yield return null;
            yield return null;
            SelectInventory(_selectedCharacter);
        }

        public void SelectInventory(CharacterEnum character)
        {
            onSelectInventory?.Invoke(_inventory[character]);

            ChipsetInventory prevInventory = GetInventory(_selectedCharacter);

            if (prevInventory != null && _selectedCharacter != character)
                prevInventory.DisableInventory();

            _selectedCharacter = character;

            ChipsetInventory currentInventory = GetInventory(_selectedCharacter);
            currentInventory.EnableInventory();
        }

        public List<Vector2Int> GetOpenedInventorySlots() => _openInventory;

        public ChipsetInventory GetInventory(CharacterEnum character) => _inventory[character];
    }
}
