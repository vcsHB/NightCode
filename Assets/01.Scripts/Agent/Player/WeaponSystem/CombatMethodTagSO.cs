using UnityEngine;
namespace Agents.Players.WeaponSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerWeapon/CombatMethodSO")]
    public class CombatMethodTagSO : ScriptableObject
    {
        public Sprite methodIconSprite;
        public string methodName;
        public string methodDescription;
        public Color methodColor;
         
    }
}