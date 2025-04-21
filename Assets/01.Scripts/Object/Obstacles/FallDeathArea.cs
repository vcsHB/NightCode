using Combat;
using Combat.PlayerTagSystem;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage
{

    public class FallDeathArea : MonoBehaviour
    {
        public UnityEvent OnEnterEvent;
        [SerializeField] private CombatData _combatData;
        [SerializeField] private Transform _backPoint;
        private PlayerCombatManager _playerCombatManager;
        private void Start()
        {
            _playerCombatManager = PlayerManager.Instance.GetCompo<PlayerCombatManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerManager.Instance.SetCurrentPlayerPosition(_backPoint.position);

            OnEnterEvent?.Invoke();
            _playerCombatManager.ApplyDamagePlayer(_combatData);
        }

    }

}