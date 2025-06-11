using System;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerCombatEnergyController : MonoBehaviour, IAgentComponent
    {
        public event Action<int, int> OnEnergyChangedEvent;
        [SerializeField] private int _currentEnergy;
        [SerializeField] private int _maxEnergy;

        public void Initialize(Agent agent)
        {
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void SetEnergyData(int current, int max)
        {
            _currentEnergy = current;
            _maxEnergy = max;

            InvokeEnergyChange();
        }

        public bool IsEnough(int amount) => _currentEnergy >= amount;
        public void GainEnergy(int amount)
        {
            _currentEnergy += amount;
            InvokeEnergyChange();
        }
        public bool TryUseEnergy(int amount)
        {
            if (_currentEnergy >= amount)
            {
                _currentEnergy -= amount;
                InvokeEnergyChange();
                return true;
            }
            return false;
        }

        private void InvokeEnergyChange()
        {
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergy);
            OnEnergyChangedEvent?.Invoke(_currentEnergy, _maxEnergy);
        }


    }
}