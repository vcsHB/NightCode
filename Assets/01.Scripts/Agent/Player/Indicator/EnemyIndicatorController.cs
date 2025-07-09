using Agents.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Agents.Players
{
    public class EnemyIndicatorController : MonoBehaviour
    {
        [SerializeField] private EnemyIndicator _indicateObject;
        [SerializeField]private Transform _playerTrm;

        private List<Enemy> _enemyToIndicate = new List<Enemy>();
        private Dictionary<Enemy, EnemyIndicator> _enemyIndicatorDict = new Dictionary<Enemy, EnemyIndicator>();


        public void AddEnemy(Enemy enemy)
        {
            if (_enemyToIndicate.Contains(enemy)) return;

            EnemyIndicator indicatorInstance = Instantiate(_indicateObject, transform);
            indicatorInstance.Initialize(_playerTrm, enemy.transform);
            indicatorInstance.OnRemoveIndicator += () => RemoveEnemy(enemy);
            enemy.OnDieEvent += () => RemoveEnemy(enemy);

            _enemyToIndicate.Add(enemy);
            _enemyIndicatorDict.Add(enemy, indicatorInstance);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            if (enemy == null || _enemyToIndicate.Contains(enemy) == false) return;

            enemy.OnDieEvent -= () => RemoveEnemy(enemy);
            _enemyToIndicate.Remove(enemy);
            
            Destroy(_enemyIndicatorDict[enemy].gameObject);
            _enemyIndicatorDict.Remove(enemy);
        }
    }
}
