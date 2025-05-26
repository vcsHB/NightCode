using UnityEngine;
namespace Agents.Players.WeaponSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerWeapon/WeaponSO")]
    public class PlayerWeaponSO : ScriptableObject
    {
        public int id;
        public string weaponName;
        public string weaponDescription;
        public Sprite weaponIcon;

        public PlayerWeapon weaponPrefab;

        public void SetId(int newId)
        {
            id = newId;
        }
    }
}