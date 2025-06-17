using Combat.PlayerTagSystem;
using StatSystem;
using UnityEngine;
namespace Agents.Players.ChipsetSystem
{

    public class Solitude : ChipsetFunction
    {
        private StatSO damageStat;
        [SerializeField] private float _increaseAmount = 5f;
        [SerializeField] private int _conditionMemberCharacterAmount = 1;
        public override void Initialize(Player owner, EnvironmentData enviromentData)
        {
            base.Initialize(owner, enviromentData);
            damageStat = _status.GetStat(StatusEnumType.Attack);
            if (enviromentData.charatcerAmount == _conditionMemberCharacterAmount)
            {
                damageStat.AddBuffDebuff(this, _increaseAmount);

            }
        }
    }
}