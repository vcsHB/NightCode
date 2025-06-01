using System;
using System.Collections.Generic;
using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Agents.Players.WeaponSystem.Weapon
{

    public class BattleAxeWeapon : PlayerWeapon
    {
        public UnityEvent OnAxeThrowEvent;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private BattleAxe _battleAxePrefab;
        [SerializeField] private int _throwAmount = 1;
        private Queue<BattleAxe> _axePool = new();
        private int _currentAxeAmount;

        public override void Initialize(Player player)
        {
            base.Initialize(player);
            _animationTrigger.OnRopeTurboEvent.AddListener(HandleAttack);
            _currentAxeAmount = _throwAmount;

            for (int i = 0; i < _throwAmount; i++)
            {
                BattleAxe newAxe = Instantiate(_battleAxePrefab);
                newAxe.SetActive(false);
                newAxe.Initialize(_player.transform);
                _axePool.Enqueue(newAxe);
            }
        }

        public BattleAxe GetAxe()
        {
            if (_currentAxeAmount <= 0) return null;
            if (_axePool.Count <= 0) return null;

            BattleAxe axe = _axePool.Dequeue();
            axe.OnAxeReturnedEvent += HandleAxeReturned;
            _currentAxeAmount--;
            return axe;
        }

        private void HandleAxeReturned(BattleAxe axe)
        {
            axe.OnAxeReturnedEvent -= HandleAxeReturned;
            _currentAxeAmount++;
            _axePool.Enqueue(axe);
        }

        public override void HandleAttack()
        {
            if (_currentAxeAmount <= 0) return;
            Collider2D[] targets = _targetDetector.DetectTargetsSorted();
            for (int i = 0; i < targets.Length; i++)
            {
                if (i > _throwAmount) break;
                BattleAxe axe = GetAxe();
                if (axe == null) return;
                
                axe.transform.position = transform.position;
                Vector2 direction = targets[i].transform.position - transform.position;
                axe.Throw(direction.normalized);
                OnAxeThrowEvent?.Invoke();
            }
        }
    }
}