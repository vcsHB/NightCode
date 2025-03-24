using System;
using HUDSystem;
using UnityEngine;
namespace Agents.Players
{

    public class StaminaController : MonoBehaviour, IAgentComponent
    {
        [SerializeField] private int _maxStamina;
        [SerializeField] private int _currentStamina;
        [SerializeField] private float _recoverDuration;
        private float _currentRecoverTime;
        private float _lastRecoverTime;
        private Player _owner;
        private StaminaHUD _hud;

        #region AgentComponent Functions

        public void Initialize(Agent agent)
        {
            _owner = agent as Player;
            _owner.OnEnterEvent += HandleOwnerEnterState;
            _owner.OnExitEvent += HandleOwnerExitState;

            _hud = HUDController.Instance.GetHUD<StaminaHUD>();
        }

        public void AfterInit() { }

        public void Dispose()
        {
            _owner.OnEnterEvent -= HandleOwnerEnterState;
            _owner.OnExitEvent -= HandleOwnerExitState;

        }

        #endregion

        private void Update()
        {
            if (_currentStamina >= _maxStamina) return;

            _currentRecoverTime += Time.deltaTime;
            if (_currentRecoverTime > _recoverDuration)
            {
                _currentRecoverTime = 0f;
                _lastRecoverTime = Time.time;
                _currentStamina++;
                HandleRefreshUI();
            }
        }

        public void ReduceStamina()
        {
            _currentStamina--;
            HandleRefreshUI();
        }

        public bool CheckEnough(int needValue)
        {
            return _currentStamina >= needValue;
        }


        private void HandleOwnerEnterState()
        {
            _hud.Open();
            float afterTime = Time.time - _lastRecoverTime;
            _currentStamina += (int)(afterTime / _currentRecoverTime);
            _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
            HandleRefreshUI();
        }
        private void HandleRefreshUI()
        {
            _hud.HandleRefresh(_currentStamina, _maxStamina);

        }
        private void HandleOwnerExitState()
        {
            _hud.Close();
        }



    }
}