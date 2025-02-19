using UnityEngine;

namespace ObjectPooling
{
    public interface IPoolable
    {
        public PoolingType type { get; set; }
        public GameObject ObjectPrefab { get; }
        
        public void ResetItem();
    }
}