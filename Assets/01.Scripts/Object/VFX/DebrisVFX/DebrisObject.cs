using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage.VFX
{
    [System.Serializable]
    public struct DebrisData
    {
        public Sprite debrisSprite;
        public bool isCircleCollider;
        public float shardSize;
    }
    public class DebrisObject : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }
        [SerializeField] private DebrisData[] _debrisDatas;
        [SerializeField] private DebrisShard _shardPrefab;
        [SerializeField] private Vector2 velocityOffset;
        [SerializeField] private float _explodeMaxPower;
        [SerializeField] private float _explodeMinPower;
        [SerializeField] private float _lifeTime;
        private ushort _deadShardCount = 0;


        private DebrisShard[] _shards;

        public GameObject ObjectPrefab => gameObject;


        private void Awake()
        {
            _shards = new DebrisShard[_debrisDatas.Length];
            for (int i = 0; i < _debrisDatas.Length; i++)
            {
                DebrisShard shard = Instantiate(_shardPrefab, transform);
                shard.Initialize(_debrisDatas[i]);
                shard.OnShardDeadEvent += HandleShardDead;

                _shards[i] = shard;
                shard.gameObject.SetActive(false);
            }
        }

        [ContextMenu("DebugPlay")]
        public void Play()
        {
            _deadShardCount = 0;
            foreach (DebrisShard shard in _shards)
            {
                shard.gameObject.SetActive(true);
                shard.transform.position = transform.position;
                shard.Play(Random.insideUnitCircle + velocityOffset, Random.Range(_explodeMinPower, _explodeMaxPower), _lifeTime);
            }
        }

        private void HandleShardDead()
        {
            _deadShardCount++;
            if (_deadShardCount >= _shards.Length)
            {
                PoolManager.Instance.Push(this);
            }
        }


        public void ResetItem()
        {

        }
    }

}