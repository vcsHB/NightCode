namespace Agents.Players.WeaponSystem
{
    [System.Serializable]
    public class WeaponData
    {
        public int id;
        public int selectedCharacter;

        public WeaponData(int id, int character)
        {
            this.id = id;
            this.selectedCharacter = character;
        }
    }
}