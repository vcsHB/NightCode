using Agents.Players.WeaponSystem;
using UnityEngine;

namespace Core.DataControl
{
    [CreateAssetMenu(menuName = "Character/CharacterDataInitializeSO", fileName = "CharacterDataInitializeSO")]
    public class CharacterDataInitializeSO : ScriptableObject
    {
        public PlayerWeaponSO anInitializeWeapon;
        public PlayerWeaponSO jinInitializeWeapon;
        public PlayerWeaponSO binaInitializeWeapon;
    }
}
