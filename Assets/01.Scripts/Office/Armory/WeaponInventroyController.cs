using System.Collections.Generic;
using Combat.SubWeaponSystem;
using UnityEngine;

namespace Office.Armory
{

    public class WeaponInventroyController : MonoBehaviour
    {
        [SerializeField]
        private List<SubWeaponData> _dataList;


        public bool IsUnlocked(int id)
        {
            SubWeaponData data = GetWeapon(id);
            return data.isEnabled;
        }

        public SubWeaponData GetWeapon(int id)
        {
            for (int i = 0; i < _dataList.Count; i++)
            {
                if (_dataList[i].id == id)
                    return _dataList[i];
            }
            SubWeaponData newWeaponData = new SubWeaponData();
            newWeaponData.id = id;
            _dataList.Add(newWeaponData);
            return newWeaponData;
        }

        public void UnlockWeapon(int id)
        {
            SubWeaponData data = GetWeapon(id);
            data.isEnabled = true;
            data.level = 1;
        }


        public void UpgradeWeapon(int id)
        {
            if (!IsUnlocked(id)) return;
        }
    }

}