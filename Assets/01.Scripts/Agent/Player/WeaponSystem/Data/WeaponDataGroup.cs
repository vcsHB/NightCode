namespace Agents.Players.WeaponSystem
{
    [System.Serializable]
    public class WeaponDataGroup
    {
        public WeaponData[] weaponDatas;

        public WeaponData GetWeaponData(int id)
        {
            if (id < 0 || id > weaponDatas.Length - 1)
                return null;

            return weaponDatas[id];
        }
    }
}