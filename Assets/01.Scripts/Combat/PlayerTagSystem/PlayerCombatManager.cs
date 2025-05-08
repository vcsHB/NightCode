using Agents.Players;
using UnityEngine;
namespace Combat.PlayerTagSystem
{
    public class PlayerCombatManager : MonoBehaviour, IPlayerSubManager
    {
        private PlayerManager _playerManager;

        public void Initialize(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void ApplyDamagePlayer(CombatData combatData)
        {
            _playerManager.CurrentPlayer.HealthCompo.ApplyDamage(combatData);
        }

        public void ApplyDamageAllPlayer(CombatData combatData)
        {
            foreach(Player player in _playerManager.playerList)
            {
                player.HealthCompo.ApplyDamage(combatData);
            }
        }

    }
}