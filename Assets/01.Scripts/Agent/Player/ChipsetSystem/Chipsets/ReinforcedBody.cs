using Combat;
using Combat.PlayerTagSystem;
using StatSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class ReinforcedBody : ChipsetFunction
    {
        private Health _health;
        private StatSO healthStat;
        [SerializeField] private float _increaseAmount = 30f;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            _health = owner.HealthCompo;
            healthStat = _status.GetStat(StatusEnumType.Health);
            healthStat.AddBuffDebuff(this, _increaseAmount);
            
        }

    }
}