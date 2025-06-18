using Agents.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Agents.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyPoolGroupSO poolGroup;

        private Dictionary<string, Queue<Enemy>> _enemyDic;

        private void Awake()
        {
            _enemyDic = new Dictionary<string, Queue<Enemy>>();
            poolGroup.enemyPool.ForEach(pool =>
            {
                Queue<Enemy> q = new Queue<Enemy>();

                for (int i = 0; i < pool.poolCount; i++)
                {
                    Enemy enemy = Instantiate(pool.enemyPf, transform);
                    enemy.gameObject.SetActive(false);
                    enemy.Init(pool.poolName);
                    q.Enqueue(enemy);
                }

                _enemyDic.Add(pool.poolName, q);
            });
        }

        public Enemy Pop(string name)
        {
            if (_enemyDic.TryGetValue(name, out Queue<Enemy> enemyQueue))
            {
                if (enemyQueue.TryDequeue(out Enemy enemy))
                {
                    enemy.gameObject.SetActive(true);
                    return enemy;
                }
                else
                {
                    Debug.LogError($"enemy {name} count is not enough");
                }
            }
            else
            {
                Debug.LogError($"enemy {name} is not exsist");
            }

            return null;
        }

        public void Push(string name,  Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            _enemyDic[name].Enqueue(enemy);
        }
    }
}
