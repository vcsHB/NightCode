using UnityEngine;

namespace Agents.Enemies
{
    [CreateAssetMenu(menuName = "SO/Combat/EnemyPool")]
    public class EnemyPoolSO : ScriptableObject
    {
        public string poolName;
        public Enemy enemyPf;
        public int poolCount;
    }
}
