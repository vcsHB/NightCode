using UnityEngine;
using UnityEngine.Events;

namespace Agents.Enemies.BossManage
{
    [System.Serializable]
    public struct PhaseData
    {
        public UnityEvent OnPhaseEnterEvent;
        public float healthCondition; // ratio Value

    }
    public class BossPhaseController : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnPhaseChangeEvent;
        [SerializeField] private PhaseData[] _phaseDatas;
        [SerializeField] private int _currentPhase = 0;

        public int CurrentPhase => _currentPhase;
        public int MaxPhase => _phaseDatas.Length;
        protected Boss _boss;

        public void Initialize(Agent agent)
        {
            _boss = agent as Boss;
            _boss.HealthCompo.OnHealthChangedValueEvent += HandleHealthChange;
        }
        public void AfterInit() { }

        public void Dispose()
        {
            _boss.HealthCompo.OnHealthChangedValueEvent -= HandleHealthChange;

        }


        private void HandleHealthChange(float current, float max)
        {
            float ratio = current / max;
            if (_phaseDatas[_currentPhase].healthCondition >= ratio)
            {
                SetNextPhase();
            }
        }


        public void SetNextPhase()
        {
            if (_currentPhase >= MaxPhase) return;
            _currentPhase++;
            OnPhaseChangeEvent?.Invoke();
            _phaseDatas[_currentPhase].OnPhaseEnterEvent?.Invoke();
        }
    }

}