namespace Agents.Players.WeaponSystem
{
    [System.Serializable]
    public class WeaponDataGroup
    {
        public WeaponData[] weaponDatas;

        public WeaponData GetWeaponData(int index)
        {
            if (index < 0 || index > weaponDatas.Length - 1)
                return null;

            return weaponDatas[index];
        }

        public WeaponData GetWeaponData(CharacterEnum character)
        {
            for (int i = 0; i < weaponDatas.Length; i++)
            {
                if ((CharacterEnum)weaponDatas[i].selectedCharacter == character)
                    return weaponDatas[i];
            }
            return null;
        }

        public WeaponData GetWeaponDataById(int id)
        {
            for (int i = 0; i < weaponDatas.Length; i++)
            {
                if (weaponDatas[i].id == id)
                {
                    return weaponDatas[i];
                }
            }

            return null;
        }
    }
}