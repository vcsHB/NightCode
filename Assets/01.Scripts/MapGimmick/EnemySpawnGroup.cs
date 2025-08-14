using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Agents.Enemies
{
    public class EnemySpawnGroup : MonoBehaviour
    {
        public UnityEvent OnCompleteWave;
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private int _maxWave;

        private List<Enemy> _enemyList;
        private List<EnemySpawnPoint> _spawnPoints;
        private int _currentWave = 0;

        public int MaxWave => _maxWave;
        public int CurrentWave => _currentWave;


        private void Awake()
        {
            _enemyList = new List<Enemy>();
            _spawnPoints = GetComponentsInChildren<EnemySpawnPoint>().ToList();
        }

        private void OnDisable()
        {
            _enemyList.ForEach(enemy => enemy.OnDisableBody -= OnEnemyDisable);
        }

        public void StartWave()
        {
            _spawnPoints.ForEach(point => point.Spawn());
        }

        public void SpawnEnemy(Vector2 position, string name)
        {
            Enemy enemy = _spawner.Pop(name);
            enemy.transform.position = position;
            enemy.OnDieEvent += () =>
            {
                _enemyList.Remove(enemy);

                if (_enemyList.Count <= 0)
                {
                    _currentWave++;

                    if (_currentWave >= MaxWave)
                    {
                        Debug.Log("³¡³­");
                        OnCompleteWave?.Invoke();
                        return;
                    }
                    _spawnPoints.ForEach(point => point.Spawn());
                }
            };
            enemy.OnDisableBody += OnEnemyDisable;
            _enemyList.Add(enemy);
        }

        private void OnEnemyDisable(Enemy enemy, string name)
        {
            enemy.OnDisableBody -= OnEnemyDisable;
            _spawner.Push(name, enemy);
        }
    }
}
