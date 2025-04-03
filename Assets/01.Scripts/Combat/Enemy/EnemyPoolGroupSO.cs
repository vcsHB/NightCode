using System.Collections.Generic;
using UnityEngine;

namespace Agents.Enemies
{
    [CreateAssetMenu(menuName = "SO/Combat/EnemyPoolGroup")]
    public class EnemyPoolGroupSO : ScriptableObject
    {
        public List<EnemyPoolSO> enemyPool;

        public EnemyPoolSO GetPoolSO(string name)
        {
            if (enemyPool.Exists(pool => pool.poolName == name) == false)
            {
                Debug.LogWarning($"Enemy pool named {name} not exsist");
                return null;
            }

            return enemyPool.Find(pool => pool.poolName == name);
        }
    }
}
