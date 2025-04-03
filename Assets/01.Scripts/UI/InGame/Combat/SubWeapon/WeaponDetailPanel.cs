using Combat.SubWeaponSystem;
using UnityEngine;
namespace UI.InGame.GameUI.Combat.SubWeaponSystem
{

    public abstract class WeaponDetailPanel : UIPanel
    {
        [field:SerializeField] public SubWeaponType SubWeaponType;

        public abstract void SetData(SubWeaponSO data);

        public abstract void HandleWeaponCountChange(int currentCount, int maxCount);
    }
}