using System.Collections.Generic;
using UnityEngine;

namespace Agents.Enemies
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public List<EnemyType> spawnEnemyByWave;
        private EnemySpawnGroup _spawnGroup;

        private void Awake()
        {
            _spawnGroup = GetComponentInParent<EnemySpawnGroup>();
        }

        public void Spawn()
        {
            int currentWave = _spawnGroup.CurrentWave;
            if (spawnEnemyByWave.Count <= currentWave) return;

            EnemyType enemyType = spawnEnemyByWave[currentWave];
            if (enemyType == EnemyType.None) return;

            _spawnGroup.SpawnEnemy(transform.position, enemyType.ToString());
        }
    }

    public enum EnemyType
    {
        None,
        DummyShooter,
        LaserShooter,
    }
}
