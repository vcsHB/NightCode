using Combat.PlayerTagSystem;
using StatSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class CombatTextbook : ChipsetFunction
    {
        [SerializeField] private float _increaseAmount = 5f;
        
        private StatSO damageStat;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            damageStat = _status.GetStat(StatusEnumType.Attack);
            damageStat.AddBuffDebuff(this, _increaseAmount);
        }
    }
}