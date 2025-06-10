using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.SkillSystem
{

    public abstract class PlayerSkill : MonoBehaviour
    {
        public UnityEvent OnSkillUsedEvent;
        protected Player _player;
        protected PlayerCombatEnergyController _energyController;

        public virtual void Initialize(Player player)
        {
            _player = player;
            _energyController = _player.GetCompo<PlayerCombatEnergyController>();
        }

        public void HandleUseSkill()
        {
            OnSkillUsedEvent?.Invoke();
            UseSkill();
        }
        protected abstract void UseSkill();
    }
}