using Agents.Players;
using UnityEngine;
namespace Combat.PlayerTagSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerManage/Player")]
    public class PlayerSO : ScriptableObject
    {
        public int id;
        public string characterName;
        public Sprite characterIconSprite;
        public Color personalColor;
        public Player playerPrefab;

        public void SetId(int newID)
        {
            id = newID;
        }

    }
}