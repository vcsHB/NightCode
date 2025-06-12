using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Chipset
{
    public class ChipsetTable : MonoBehaviour
    {
        public event Action<CharacterEnum> onSelectInventory;

        public ChipsetGruopSO chipsetGruopSO;
        private Dictionary<CharacterEnum, ChipsetInventory> _inventory;
        private CharacterEnum _selectedCharacter;

        private List<Vector2Int> _openInventory;

        public CharacterEnum SelectedCharacter => _selectedCharacter;

        public void Init(List<Vector2Int> openedInventory)
        {
            _openInventory = openedInventory;
            _inventory = new Dictionary<CharacterEnum, ChipsetInventory>();
            var inventoryList = GetComponentsInChildren<ChipsetInventory>();

            for (int i = 0; i < inventoryList.Length; i++)
            {
                _inventory.Add((CharacterEnum)i, inventoryList[i]);
                inventoryList[i].Init(_openInventory);
                inventoryList[i].DisableInventory();
            }

            SelectInventory(CharacterEnum.An);
        }

        public void SelectInventory(CharacterEnum character)
        {
            onSelectInventory?.Invoke(character);

            ChipsetInventory prevInventory = GetInventory(_selectedCharacter);

            if (prevInventory != null && _selectedCharacter != character)
                prevInventory.DisableInventory();

            _selectedCharacter = character; 
        }

        public List<Vector2Int> GetOpenedInventorySlots() => _openInventory;

        public ChipsetInventory GetInventory(CharacterEnum character)
            => _inventory[character];
    }
}
