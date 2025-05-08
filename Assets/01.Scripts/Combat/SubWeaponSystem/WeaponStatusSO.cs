namespace Combat.SubWeaponSystem
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "SO/Combat/SubWeapons/WeaponStatusSO")]
    public class WeaponStatusSO : ScriptableObject
    {
        public string statusName;
        public string description;
        public Sprite statusIcon;
    }
}