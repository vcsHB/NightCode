using Agents.Players;
using UnityEngine;
namespace Combat.PlayerTagSystem
{
    [CreateAssetMenu(menuName ="SO/PlayerManage/Player")]
    public class PlayerSO : ScriptableObject
    {
        public int id;
        public string characterName;
        public Player playerPrefab;


    }
}