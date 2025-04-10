using Combat.SubWeaponSystem;
using UnityEngine;
namespace UI.OfficeScene.Armory
{

    public abstract class WeaponInformationPanel : MonoBehaviour
    {
        public abstract void SetWeaponData(SubWeaponSO weapon, SubWeaponData weaponData);
    }
}