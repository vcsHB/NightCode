using Agents.Players.WeaponSystem;
using Chipset;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DataControl
{
    [CreateAssetMenu(menuName = "Character/CharacterDataInitializeSO", fileName = "CharacterDataInitializeSO")]
    public class CharacterDataInitializeSO : ScriptableObject
    {
        public int characterDefaultHealth = 100;
        public PlayerWeaponSO anInitializeWeapon;
        public PlayerWeaponSO jinInitializeWeapon;
        public PlayerWeaponSO binaInitializeWeapon;

        public List<Vector2Int> openInventory;
        public List<ChipsetSO> containChipsets;
    }
}
