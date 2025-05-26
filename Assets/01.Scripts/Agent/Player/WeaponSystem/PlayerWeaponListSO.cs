using UnityEngine;
namespace  Agents.Players.WeaponSystem
{

    [CreateAssetMenu(menuName = "SO/PlayerWeapon/WeaponList")]
    public class PlayerWeaponListSO : ScriptableObject
    {
        public PlayerWeaponSO[] weapons;
        
        public PlayerWeaponSO GetWeapon(int id)
        {
            if (id >= weapons.Length || id < 0)
            {
                Debug.LogError("Not Exist Id in weapon List");
                return null;
            }
            return weapons[id];
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (weapons == null) return;

            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] == null) break;
                weapons[i].SetId(i);
            }
        }
#endif
    }
}